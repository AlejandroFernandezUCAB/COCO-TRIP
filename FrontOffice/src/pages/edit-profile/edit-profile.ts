import { Component } from '@angular/core';
import {  IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ChangepassPage} from '../changepass/changepass';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { transition } from '@angular/core/src/animation/dsl';
import { Usuario } from '../../dataAccessLayer/domain/usuario';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
import { DateTime } from 'ionic-angular/components/datetime/datetime';
import { ComandoEditarPerfil } from '../../businessLayer/commands/comandoEditarPerfil';
import { File } from '@ionic-native/file';
import { Transfer, TransferObject } from '@ionic-native/transfer';
import { FilePath } from '@ionic-native/file-path';
import { Camera } from '@ionic-native/camera';
// storage no es necesario, pues el objeto se pasa por parametros
//import { Storage } from '@ionic/storage'; 

declare var cordova: any;

@IonicPage()
@Component({
  selector: 'page-edit-profile',
  templateUrl: 'edit-profile.html',
})
export class EditProfilePage {  

  change = ChangepassPage;
  myForm: FormGroup;

  usuario : Usuario;
  nombreAntiguo : string;
  apellidoAntiguo : string;
  fechaAntiguo : Date;
  generoAntiguo : string;


  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, public fb: FormBuilder, 
    private comando: ComandoEditarPerfil, private camera: Camera, private transfer: Transfer, private file: File, private filePath: FilePath )
  {
    // se obtiene el genero del usuario
    this.usuario = FabricaEntidad.crearUsuarioConParametros(
      0, 'Nombre', 'Apellido', 'Correo', 'default', new Date('1995-04-11T00:00:00.196Z'), null, "F" )
    //this.genero = this.usuario.Genero;
    console.log(navParams);
    // obtengo los datos recibidos de la vista anterior
    // la verificacion de la fecha permite evitar la excepcion de RangeError
    if(navParams.data != 0 && navParams.data.getFechaNacimiento != undefined){
      // inyectando datos al objeto usuario
      this.usuario = navParams.data as Usuario;
      this.nombreAntiguo = this.usuario.getNombre;
      this.apellidoAntiguo = this.usuario.getApellido;
      this.generoAntiguo = this.usuario.getGenero;
      this.fechaAntiguo = this.usuario.getFechaNacimiento;
      //this.usuario.setFechaNacimiento = new Date(navParams.data.getFechaNacimiento);
      //this.genero = navParams.data.Genero;
    }

    //inyecto los datos en el formulario
    this.myForm = this.fb.group(
      {
        nombre: [this.usuario.getNombre, [Validators.required]],
        apellido: [this.usuario.getApellido,[Validators.required]],
        genero: [this.usuario.getGenero],
        fechanac: [new Date(this.usuario.getFechaNacimiento).toISOString(),[Validators.required]]
      }
    )


  }

  // function ejecutada al hacer submit del formulario
  saveData(){
    // inyectamos datos nuevos
    this.usuario.setNombre = this.myForm.value.nombre;
    this.usuario.setApellido = this.myForm.value.apellido;
    // notar que el toISOString es muy importante para que el apirest reconozca a FechaNacimiento
    // como un valor de DateTime valido 
    this.usuario.setFechaNacimiento = new Date(this.myForm.value.fechanac);
    this.usuario.setGenero = this.myForm.value.genero;

    this.comando._entidad = this.usuario;
    this.comando.execute().then(data => {
      let apiRestResponse = data;
      this.regresarAvistaAnterior(data);

    }, error => {   
      
      this.usuario.setNombre = this.nombreAntiguo;
      this.usuario.setApellido = this.apellidoAntiguo;
      this.usuario.setGenero = this.generoAntiguo;
      this.usuario.setFechaNacimiento = this.fechaAntiguo;
      this.regresarAvistaAnterior(false);

    });

  }

  // funcion para retornar el resultado de la operacion en un toast
  // y luego volver a la vista anterior
  regresarAvistaAnterior(apiRestResponse){
    this.showToastWithCloseButton(apiRestResponse);
    this.navCtrl.pop();
  }

  // funcion para mostrar un mensaje toast en pantalla
  showToastWithCloseButton(apiRestResponse) {
    // se determina el mensaje a mostrar
    let result;
    if (apiRestResponse == true) {
      result = this.translateToastMessage("Se guardaron tus cambios");
    }
    else if (apiRestResponse == "datosIguales") {
        result = this.translateToastMessage("No hay cambios en los campos");
    }
    else{
        result = this.translateToastMessage("Error Modificando los datos");
    }
    
    const toast = this.toastCtrl.create({
      message: result ,
      duration: 2000,
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
    // se muestra el mensaje
    toast.present();
  }

  translateToastMessage(message){
    let translation;
    this.translateService.get(message).subscribe(
      value => {
        translation = value;
      }
    );
    return translation;
  }

  //Este OpenMenu es para el ActionSheet de cambiar foto
  openMenu() {
    let actionSheet = this.actionsheetCtrl.create({
      title: 'Albums',
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: 'Borrar',
          role: 'destructive',
          icon: !this.platform.is('ios') ? 'trash' : null,
          handler: () => {
            console.log('Delete clicked');
          }
        },
        {
          text: 'Usar Camara',
          icon: !this.platform.is('ios') ? 'albums' : null,
          handler: () => {
            console.log('Change clicked');
          }
        },
        {
          text: 'Foto de galeria',
          icon: !this.platform.is('ios') ? 'albums' : null,
          handler: () => {
            console.log('Change clicked');
            this.tomarFoto(this.camera.PictureSourceType.PHOTOLIBRARY);
          }
        },
        {                                                                                       
          text: 'Cancelar',
          role: 'cancel', // will always sort to be on the bottom
          icon: !this.platform.is('ios') ? 'close' : null,
          handler: () => {
            console.log('Cancel clicked');
            this.tomarFoto(this.camera.PictureSourceType.CAMERA);
          }
        }
      ]
    });
    actionSheet.present();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad EventosActividadesPage');
    
  }

  public tomarFoto(sourceType){
    var opciones = {
      quality: 100,
      sourceType: sourceType,
      saveToPhotoAlbum: false,
      correctOrientation: true
    };

    // Get the data of an image
    this.camera.getPicture(opciones).then((imagePath) => {
      // Special handling for Android library
      if (this.platform.is('android') && sourceType === this.camera.PictureSourceType.PHOTOLIBRARY) {
        this.filePath.resolveNativePath(imagePath)
        .then(filePath => {
          let correctPath = filePath.substr(0, filePath.lastIndexOf('/') + 1);
          let currentName = imagePath.substring(imagePath.lastIndexOf('/') + 1, imagePath.lastIndexOf('?'));
          this.copyFileToLocalDir(correctPath, currentName, this.createFileName());
        });
      } else {
        var currentName = imagePath.substr(imagePath.lastIndexOf('/') + 1);
        var correctPath = imagePath.substr(0, imagePath.lastIndexOf('/') + 1);
        this.copyFileToLocalDir(correctPath, currentName, this.createFileName());
      }
    }, (err) => {
      this.presentToast('Error while selecting image.');
    });
  }

  // Create a new name for the image
  private createFileName() {
    var d = new Date(),
    n = d.getTime(),
    newFileName =  n + ".jpg";
    return newFileName;
  }

  // Copy the image to a local folder
  private copyFileToLocalDir(namePath, currentName, newFileName) {
    this.file.copyFile(namePath, currentName, cordova.file.dataDirectory, newFileName)
    .then(success => {
      this.usuario.setFoto = newFileName;
      this.presentToast('Aparentemente funciona.');
    }, error => {
      this.presentToast('Error while storing file.');
    });
  }

  // Always get the accurate path to your apps folder
  public pathForImage(img) {
    if (img === null) {
      return '';
    } else {
      return cordova.file.dataDirectory + img;
    }
  }

  private presentToast(text) {
    let toast = this.toastCtrl.create({
      message: text,
      duration: 3000,
      position: 'top'
    });
    toast.present();
  }

  /*

  public uploadImage() {
  // Destination URL
  var url = "http://yoururl/upload.php";
 
  // File for Upload
  var targetPath = this.pathForImage(this.lastImage);
 
  // File name only
  var filename = this.lastImage;
 
  var options = {
    fileKey: "file",
    fileName: filename,
    chunkedMode: false,
    mimeType: "multipart/form-data",
    params : {'fileName': filename}
  };
 
  const fileTransfer: TransferObject = this.transfer.create();
 
  this.loading = this.loadingCtrl.create({
    content: 'Uploading...',
  });
  this.loading.present();
 
  // Use the FileTransfer to upload the image
  fileTransfer.upload(targetPath, url, options).then(data => {
    this.loading.dismissAll()
    this.presentToast('Image succesful uploaded.');
  }, err => {
    this.loading.dismissAll()
    this.presentToast('Error while uploading file.');
  });
}

  */

}

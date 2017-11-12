import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, AlertController,ActionSheetController, Platform, Loading } from 'ionic-angular';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HomePage } from '../home/home';
import { LoginPage } from '../login/login';
import { LoadingController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { MenuController } from 'ionic-angular';
import { Facebook } from '@ionic-native/facebook';
import { File } from '@ionic-native/file';
import { Transfer, TransferObject } from '@ionic-native/transfer';
import { FilePath } from '@ionic-native/file-path';
import { Camera } from '@ionic-native/camera';
declare var cordova: any;
/**
 * Generated class for the RegisterPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-register',
  templateUrl: 'register.html',
})
export class RegisterPage {

  myForm: FormGroup;
  vista: boolean;
  toast: any;
  userData: any;
  alert: any;
  loading: any;
  nombreUsuario: string;
  correo: string;
  nombre: string;
  apellido: string;
  genero: string;
  fechaNacimiento: string;
  foto: ByteString;
  clave: string;
  clavev: string;
  lastImage: string = null;
  loadingf: Loading;
  

  //constructor(public navCtrl: NavController, public navParams: NavParams)
  constructor(public navCtrl: NavController, public loadingCtrl: LoadingController, public toastCtrl: ToastController,
    public alertCtrl: AlertController, public restapiService: RestapiService, public menu: MenuController,
    public formBuilder: FormBuilder, private storage: Storage, public facebook: Facebook, public navParams: NavParams,private camera: Camera, private transfer: Transfer, private file: File, private filePath: FilePath,public actionSheetCtrl: ActionSheetController,public platform: Platform) {
    this.myForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú0-9])+$/), Validators.minLength(5), Validators.maxLength(20)]],
      name: ['', [Validators.required, Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/), Validators.minLength(5), Validators.maxLength(30)]],
      lastName: ['', [Validators.required, Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/), Validators.minLength(5), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(30)]],
      dateBirth: ['', Validators.required],
      password: ['', [Validators.required, Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/), Validators.minLength(5), Validators.maxLength(20)]],
      passwordConfirmation: ['', [Validators.required, Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/), Validators.minLength(5), Validators.maxLength(20)]],
      gender: ['', Validators.required],
    });
    this.facebook.getLoginStatus().then(loginstatus => {
      if (loginstatus.status == "connected") {
        this.facebook.api('me?fields=id,email,first_name,last_name,birthday,picture.width(720).height(720).as(picture_large)', []).then(profile => {
          this.userData = {
            correo: profile['email'], nombre: profile['first_name'],
            apellido: profile['last_name'], fechaNacimiento: new Date(profile['birthday']).toISOString()
          };
          this.correo = this.userData.correo;
          this.nombre = this.userData.nombre;
          this.apellido = this.userData.apellido;
          this.fechaNacimiento = this.userData.fechaNacimiento;
        });
      }
    });

  }




  saveData() {
    //console.log(this.myForm.value);
    alert(JSON.stringify(this.myForm.value));
  }

  registrar() {
    this.realizarLoading();
    if (this.myForm.get('password').errors || this.myForm.get('userName').errors || this.myForm.get('name').errors || this.myForm.get('lastName').errors || this.myForm.get('email').errors || this.myForm.get('dateBirth').errors || this.myForm.get('gender').errors)
      this.realizarToast('Por favor, rellene los campos');
    else {
      if (this.clave == this.clavev) {
        this.foto = "0101";
        this.restapiService.registrarse(this.nombreUsuario, this.correo, this.nombre, this.apellido, this.genero, new Date(this.fechaNacimiento), this.clave, this.foto)
          .then(data => {
            if (data == 0 || data == -1) {
              this.realizarToast('Error, datos incorrectos.');
            }
            if (data==-2)
            {
              this.realizarToast('Registro denegado, el correo introducido se encuentra en uso');
            }
            if (data==-3)
            {
              this.realizarToast('Registro denegado, el nombre de usuario introducido se encuentra en uso');
            }
            if(data>0){
              this.realizarToast('Se le ha enviado un correo para confirmar su registro');
              this.navCtrl.setRoot(LoginPage);
            }
            this.loading.dismiss();
          });
      }
      else {
        this.realizarToast('Las Contraseñas no coinciden.');
        this.loading.dismiss();
      }
    }

  }

  realizarToast(mensaje) {
    this.toast = this.toastCtrl.create({
      message: mensaje,
      duration: 3000,
      position: 'top'
    });
    this.toast.present();
  }
  realizarLoading() {
    this.loading = this.loadingCtrl.create({
      content: 'Por favor espere...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  public presentActionSheet() {
    let actionSheet = this.actionSheetCtrl.create({
      title: 'Select Image Source',
      buttons: [
        {
          text: 'Load from Library',
          handler: () => {
            this.takePicture(this.camera.PictureSourceType.PHOTOLIBRARY);
          }
        },
        {
          text: 'Use Camera',
          handler: () => {
            this.takePicture(this.camera.PictureSourceType.CAMERA);
          }
        },
        {
          text: 'Cancel',
          role: 'cancel'
        }
      ]
    });
    actionSheet.present();
  }
  public takePicture(sourceType) {
    // Create options for the Camera Dialog
    var options = {
      quality: 100,
      sourceType: sourceType,
      saveToPhotoAlbum: false,
      correctOrientation: true
    };
   
    // Get the data of an image
    this.camera.getPicture(options).then((imagePath) => {
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
  this.file.copyFile(namePath, currentName, cordova.file.dataDirectory, newFileName).then(success => {
    this.lastImage = newFileName;
  }, error => {
    this.presentToast('Error while storing file.');
  });
}
 
private presentToast(text) {
  let toast = this.toastCtrl.create({
    message: text,
    duration: 3000,
    position: 'top'
  });
  toast.present();
}
 
// Always get the accurate path to your apps folder
public pathForImage(img) {
  if (img === null) {
    return '';
  } else {
    return cordova.file.dataDirectory + img;
  }
}

public uploadImage() {
  // Destination URL
  var url = "http://yoururl/upload.php";//aqui
 
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
 
  this.loadingf = this.loadingCtrl.create({
    content: 'Uploading...',
  });
  this.loadingf.present();
 
  // Use the FileTransfer to upload the image
  fileTransfer.upload(targetPath, url, options).then(data => {
    this.loadingf.dismissAll()
    this.presentToast('Image succesful uploaded.');
  }, err => {
    this.loadingf.dismissAll()
    this.presentToast('Error while uploading file.');
  });
}


}

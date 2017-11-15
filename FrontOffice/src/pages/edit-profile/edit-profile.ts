import { Component } from '@angular/core';
import {  IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ChangepassPage} from '../changepass/changepass';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { transition } from '@angular/core/src/animation/dsl';
// storage no es necesario, pues el objeto se pasa por parametros
//import { Storage } from '@ionic/storage'; 

@IonicPage()
@Component({
  selector: 'page-edit-profile',
  templateUrl: 'edit-profile.html',
})
export class EditProfilePage {  

  change = ChangepassPage;

  // variable asociada al campo genero en el formulario
  genero: string;
  apiRestResponse: any;
  myForm: FormGroup;

  // datos a cargar por defecto
  // se sobre escriben si la llamada al
  // apirest fue exitosa
  usuario: any = {
    Nombre: 'Nombre',
    Apellido: 'Apellido',
    FechaNacimiento: new Date('1990-04-11T00:00:00.196Z').toISOString(),
    Genero: "F"
  };


  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, public fb: FormBuilder, 
    public restapiService: RestapiService )
  {
    // se obtiene el genero del usuario
    this.genero = this.usuario.Genero;

    // obtengo los datos recibidos de la vista anterior
    // la verificacion de la fecha permite evitar la excepcion de RangeError
    if(navParams.data != 0 && navParams.data.FechaNacimiento != undefined){
      // inyectando datos al objeto usuario
      this.usuario = navParams.data;
      this.usuario.FechaNacimiento = new Date(navParams.data.FechaNacimiento).toISOString();
      this.genero = navParams.data.Genero;
    }

    //inyecto los datos en el formulario
    this.myForm = this.fb.group(
      {
        nombre: [this.usuario.Nombre, [Validators.required]],
        apellido: [this.usuario.Apellido,[Validators.required]],
        genero: [this.genero],
        fechanac: [this.usuario.FechaNacimiento,[Validators.required]]
      }
    )


  }

  // function ejecutada al hacer submit del formulario
  saveData(){
      // guardamos datos previos
    let datosPrevios = this.usuario;
      // inyectamos datos nuevos
    this.usuario.Nombre = this.myForm.value.nombre;
    this.usuario.Apellido = this.myForm.value.apellido;
    // notar que el toISOString es muy importante para que el apirest reconozca a FechaNacimiento
    // como un valor de DateTime valido 
    this.usuario.FechaNacimiento = new Date(this.myForm.value.fechanac).toISOString();
    this.usuario.Genero = this.myForm.value.genero;

    this.restapiService.modificarDatosUsuario(this.usuario).then(data =>{
        if(data != 0){
          this.apiRestResponse = data;
          this.regresarAvistaAnterior(this.apiRestResponse);
      }
      else{
        // en caso de fallo:
        // restauramos los nombres anteriores...
        this.usuario = datosPrevios;
        this.apiRestResponse = false;
        this.regresarAvistaAnterior(this.apiRestResponse);
      }
    }
    );
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
          text: 'Cambiar Foto',
          icon: !this.platform.is('ios') ? 'albums' : null,
          handler: () => {
            console.log('Change clicked');
          }
        },
        {
          text: 'Cancelar',
          role: 'cancel', // will always sort to be on the bottom
          icon: !this.platform.is('ios') ? 'close' : null,
          handler: () => {
            console.log('Cancel clicked');
          }
        }
      ]
    });
    actionSheet.present();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad EventosActividadesPage');
    
  }

}

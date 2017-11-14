import { Component } from '@angular/core';
import {  IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ChangepassPage} from '../changepass/changepass';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
//import { Storage } from '@ionic/storage'; //storage no es necesario, pues el objeto se pasa por parametros

@IonicPage()
@Component({
  selector: 'page-edit-profile',
  templateUrl: 'edit-profile.html',
})
export class EditProfilePage {

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

  change = ChangepassPage;

  genero: string;
  
  apiRestResponse: any;

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, public fb: FormBuilder, 
    public restapiService: RestapiService )
  {

    this.genero = this.usuario.Genero;

    console.log(navParams.data)
    // obtengo los datos recibidos de la vista anterior
    // la verificacion de la fecha permite evitar la excepcion de RangeError
    if(navParams.data != 0 && navParams.data.FechaNacimiento != undefined){
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

  //function ejecutada al hacer submit del formulario
  saveData(){
    // //guardamos datos previos
    // let nombreViejo = this.usuario.Nombre;
    // let apellidoViejo = this.usuario.Apellido;
    // //inyectamos datos nuevos
    // this.usuario.Nombre = this.myForm.value.nombre;
    // this.usuario.Apellido = this.myForm.value.apellido;

      //guardamos datos previos
    let datosPrevios = this.usuario;
      //inyectamos datos nuevos
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
        //restauramos los nombres anteriores
        this.usuario = datosPrevios;
        this.apiRestResponse = false;
        this.regresarAvistaAnterior(this.apiRestResponse);
      }
    }
    );
  }

  regresarAvistaAnterior(apiRestResponse){
    this.showToastWithCloseButton(apiRestResponse);
    this.navCtrl.pop();
  }

  showToastWithCloseButton(apiRestResponse) {
    let result;
    if (apiRestResponse == true) {
      this.translateService.get("Se guardaron tus cambios").subscribe(
        value => {
          // value is our translated string
          result = value;
        }
      );
    }
    else if (apiRestResponse == "datosIguales") {
      this.translateService.get("No hay cambios en los campos").subscribe(
        value => {
          // value is our translated string
          result = value;
        }
      );
    }
    else{
      this.translateService.get("Error Modificando los datos").subscribe(
        value => {
          // value is our translated string
          result = value;
        }
      );
    }
    
    const toast = this.toastCtrl.create({
      message: result ,
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
    toast.present();
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

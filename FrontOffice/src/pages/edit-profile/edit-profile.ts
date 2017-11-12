import { Component } from '@angular/core';
import {  IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ChangepassPage} from '../changepass/changepass';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage'; //para acceder a las variables que guarde en la vista de 'Editar Datos Personales'

@IonicPage()
@Component({
  selector: 'page-edit-profile',
  templateUrl: 'edit-profile.html',
})
export class EditProfilePage {

  myForm: FormGroup;
  usuario: any = {
    Nombre: 'Nombre',
    Apellido: 'Apellido'
  };
  apiRestResponse: boolean;

  public event = {
    month: '1993-02-27'
  }

  change = ChangepassPage;

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, public fb: FormBuilder, 
    private storage: Storage,public restapiService: RestapiService )
  {
    console.log(navParams.data)
    //obtengo los datos recibidos de la vista anterior
    this.usuario.Nombre = navParams.data.Nombre;
    this.usuario.Apellido = navParams.data.Apellido;
    //inyecto los datos en el formulario
    this.myForm = this.fb.group(
      {
        nombre: [this.usuario.Nombre, [Validators.required]],
        apellido: [this.usuario.Apellido,[Validators.required]]
        //sexo: [''],
      }
    )
  }

  //function ejecutada al hacer submit del formulario
  saveData(){
    
    if(this.apiRestResponse)
    {
      // alert(this.myForm.value);
      this.storage.set('nombre',this.myForm.value.nombre);
      this.storage.set('apellido',this.myForm.value.nombre);
    }
    this.navCtrl.pop();
  }

  showToastWithCloseButton() {
    const toast = this.toastCtrl.create({
      message: 'Se guardaron tus cambios',
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

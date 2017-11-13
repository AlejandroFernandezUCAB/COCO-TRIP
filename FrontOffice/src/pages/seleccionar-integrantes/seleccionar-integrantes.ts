import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,AlertController,LoadingController,ToastController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { GruposPage } from '../amistades-grupos/grupos/grupos';
import { Storage } from '@ionic/storage';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

/**
 * Generated class for the SeleccionarIntegrantesPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-seleccionar-integrantes',
  templateUrl: 'seleccionar-integrantes.html',
})
export class SeleccionarIntegrantesPage {
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
  nombreGrupo: string;
  toast: any;
  myForm: FormGroup;
  constructor(public navCtrl: NavController, public navParams: NavParams,
    public alerCtrl: AlertController,private storage: Storage,public loadingCtrl: LoadingController,
    public restapiService: RestapiService,public toastCtrl: ToastController,public formBuilder: FormBuilder) {
      this.myForm = this.formBuilder.group({
        namegroup: ['', [Validators.required]]
      });
  }


  cargando(){
    this.loading = this.loadingCtrl.create({
      content: 'Por favor espere...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  agregarGrupo(){
    if ( this.myForm.get('namegroup').errors)
    this.realizarToast('Por favor, rellene los campos');
    else{
      var nombreRestApi = this.nombreGrupo;
      this.cargando();
      this.storage.get('id').then((val) => {
      this.restapiService.agregarGrupo(val,nombreRestApi,"123")
       .then(data => {
         if (data == 0 || data == -1) {
          console.log("DIO ERROR PORQUE ENTRO EN EL IF");
   
            }
            else {
              //this.amigo = data;
              this.loading.dismiss();
              this.navCtrl.push(GruposPage);
              this.realizarToast('Agregado Grupo exitosamente');
  
            }
    
          });
        });
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

}

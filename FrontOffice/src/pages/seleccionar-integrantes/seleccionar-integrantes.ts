import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,AlertController,LoadingController,ToastController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { GruposPage } from '../amistades-grupos/grupos/grupos';
import { Storage } from '@ionic/storage';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { CrearGrupoPage } from '../crear-grupo/crear-grupo';

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
  loader: any;
  requerido: any;
  succesful: any;
  lista:any;
  numero:any;

  constructor(public navCtrl: NavController, public navParams: NavParams,
    public alerCtrl: AlertController,private storage: Storage,public loadingCtrl: LoadingController,
    public restapiService: RestapiService,public toastCtrl: ToastController,public formBuilder: FormBuilder,
    private translateService: TranslateService) {
      this.myForm = this.formBuilder.group({
        namegroup: ['', [Validators.required]]
      });
  }

  cargando(){
    this.translateService.get('Por Favor Espere').subscribe(value => {this.loader = value;})
    this.loading = this.loadingCtrl.create({
      content: this.loader,
      dismissOnPageChange: true
    });
    this.loading.present();
  }


  agregarGrupo(){
    this.translateService.get('Este campo es requerido').subscribe(value => {this.requerido = value;})
    this.translateService.get('Agregado exitosamente').subscribe(value => {this.succesful = value;})
    if ( this.myForm.get('namegroup').errors)
    this.realizarToast(this.requerido);
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
              this.restapiService.obtenerultimoGrupo()
              .then(data => {
              this.lista = data;
              this.navCtrl.push(CrearGrupoPage,{
                idGrupo: this.lista
              });
              this.loading.dismiss();
              this.realizarToast(this.succesful);
  
            }
    
       )}});
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

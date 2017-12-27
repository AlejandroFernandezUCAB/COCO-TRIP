import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,AlertController,LoadingController,ToastController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { GruposPage } from '../amistades-grupos/grupos/grupos';
import { Storage } from '@ionic/storage';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { CrearGrupoPage } from '../crear-grupo/crear-grupo';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';

//****************************************************************************************************// 
//***********************************PAGE DATOS DEL GRUPO MODULO 3************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la pagina para rellenar los datos de un grupo
 */
@IonicPage()
@Component
({
  selector: 'page-seleccionar-integrantes',
  templateUrl: 'seleccionar-integrantes.html',
})

export class SeleccionarIntegrantesPage 
{
  public loading = this.loadingCtrl.create({});
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

/**
 * Metodo que carga un loading controller al iniciar 
 * la lista de amigos
 * (Por favor espere/ please wait)
 */
  cargando(){
    this.translateService.get(Texto.CARGANDO).subscribe(value => {this.loader = value;})
    this.loading = this.loadingCtrl.create({
      content: this.loader,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

/**
 * Metodo que agrega el nombre y la foto del grupo
 */
  agregarGrupo(){
    this.translateService.get(Texto.REQUERIDO).subscribe(value => {this.requerido = value;})
    this.translateService.get(Texto.EXITO_AGREGAR_GRUPO).subscribe(value => {this.succesful = value;})
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
              this.storage.get('id').then((val) => {
                this.restapiService.obtenerultimoGrupo(val)
              .then(data => {
              this.lista = data;
              this.navCtrl.push(CrearGrupoPage,{
                idGrupo: this.lista
              });
              this.loading.dismiss();
              this.realizarToast(this.succesful);
  });
              
            }
    
       )}});
        });
      }
     }

/**
 * Metodo que despliega un toast
 * @param mensaje Texto para el toast
 */
     realizarToast(mensaje) {
      this.toast = this.toastCtrl.create({
        message: mensaje,
        duration: ConfiguracionToast.DURACION,
        position: ConfiguracionToast.POSICION
      });
      this.toast.present();
    }

}

import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, LoadingController, ToastController } from 'ionic-angular';
import { EventosCalendarioService } from '../../services/eventoscalendario';
import { HttpCProvider } from '../../providers/http-c/http-c';
import { Storage } from '@ionic/storage';

@IonicPage()
@Component({
  selector: 'page-config-notificaciones-iti',
  templateUrl: 'config-notificaciones-iti.html',
})
export class ConfigNotificacionesItiPage {
  _notif: any;
  loading:any;
  toast: any;
  IdUsuario: any;
  _itinerarios = Array();
  constructor(public navCtrl: NavController, public navParams: NavParams, public servicio: EventosCalendarioService, public http: HttpCProvider,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController,
    private storage: Storage
    ) {
    this._itinerarios= this.navParams.get('itinerarios');
    this._notif= this.navParams.get('config');
    this.storage.get('id').then((val) => {
      this.IdUsuario = val;
    });
  }

  closeModal() {
    this.navCtrl.pop();
  }


    presentLoading(){
        this.loading = this.loadingCtrl.create({
        content: 'Please wait...',
        dismissOnPageChange: true
      });
      this.loading.present();
    }

  setConfig(tipo, valor){
    console.log(tipo + " " + valor);
    this.http.modificarNotificacionCorreo(this.IdUsuario, valor).then(data =>{
      console.log(data);
    })
  }

  public realizarToast(mensaje)
  {
      this.toast = this.toastCtrl.create({
        message: mensaje,
        duration: 3000,
        position: 'middle'
      });
      this.toast.present();
  }

  updateVisible(itinerario){
    this.presentLoading();
    let up_itinerario = Array();
    this._itinerarios.forEach(iti => {
      console.log(iti);
      if (iti.Id == itinerario.Id){
        this.http.setVisible(itinerario.IdUsuario,itinerario.id,itinerario.Visible).then(
          data=> {
            if (data== 0 || data == -1){
              this.loading.dismiss();
              this.realizarToast('Por favor intente mas tarde :(');
            }else{
              this.loading.dismiss();
              iti.Visible = itinerario.Visible;
            }
          }
        )
      }
    })
  }

  ionViewWillEnter(){
    this.storage.get('id').then((val) => {
      this.IdUsuario = val;
    });
  }
}

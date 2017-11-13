import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController, ToastController, LoadingController } from 'ionic-angular';
import { HttpCProvider } from '../../providers/http-c/http-c';


@IonicPage()
@Component({
  selector: 'page-consultar-item-modal',
  templateUrl: 'consultar-item-modal.html',
})
export class ConsultarItemModalPage {

  evento: any;
  itinerario: any;
  evento1: any;
  fotos: any;
  toast: any;
  loading:any;
  base_url = '../assets/images/';
  constructor
  (
    public navCtrl: NavController,
    public navParams: NavParams,
    private viewCtrl: ViewController,
    public httpc: HttpCProvider,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController
  )
  {
    this.evento= this.navParams.get('evento');
    this.evento1=this.navParams.get('evento1');
    console.log(this.evento1);
    console.log("evento1");
    this.itinerario = this.navParams.get('itinerario');
    this.fotos = Array();
    if (this.evento1.Foto.length != 0){
      this.fotos.push(this.evento1.Foto);
    }
    else{
      this.fotos.push({
        Ruta: this.base_url+'empty-image.png'
      })
    }
  }

    public closeModal()
    {
        this.navCtrl.pop();
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

    public presentLoading()
    {
          this.loading = this.loadingCtrl.create({
          content: 'Please wait...',
          dismissOnPageChange: true
        });
        this.loading.present();
    }
}

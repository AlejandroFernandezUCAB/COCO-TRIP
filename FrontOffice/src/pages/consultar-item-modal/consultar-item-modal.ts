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
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private viewCtrl: ViewController,
    public httpc: HttpCProvider,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController
  ) {
    this.evento= this.navParams.get('evento');
    this.evento1=this.navParams.get('evento1');
    this.itinerario = this.navParams.get('itinerario');
    this.fotos = Array();
    console.log(this.evento1);
    // this.fotos = [
    //   {id: 1, foto: this.base_url+'epcot.jpg'},
    //   {id: 2, foto: this.base_url+'epcot-2.jpg'},
    //   {id: 1, foto: this.base_url+'epcot-3.jpg'},
    //   {id: 1, foto: this.base_url+'epcot-4.jpg'}
    // ]
    console.log(this.evento1.Foto);
    if (this.evento1.Foto.length != 0){
      this.fotos.push(this.evento1.Foto);
    }
    else{
      this.fotos.push({
        Ruta: this.base_url+'empty-image.png'
      })
    }
  }

  closeModal() {
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

    presentLoading()
    {
          this.loading = this.loadingCtrl.create({
          content: 'Please wait...',
          dismissOnPageChange: true
        });
        this.loading.present();
    }


    ionViewWillEnter()
    {
    }

}

import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { ToastController } from 'ionic-angular';

@IonicPage()
@Component({
  selector: 'page-preferencias',
  templateUrl: 'preferencias.html',
})
export class PreferenciasPage {

  preferenciasEnLista: any; //Aquí se guardarán los items de preferencias.
  preferenciasEnBusqueda: any; //Aquí se irán guardando los que se traigan de la Base de datos.

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController,
    public restapiService: RestapiService) {

    this.inicializarListas();

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad PreferenciasPage');
  }

  aviso(str, preferencias) {

    var posicionIndex;

    if (str == "agregado") {

      this.preferenciasEnLista.push( preferencias );
      posicionIndex = this.preferenciasEnBusqueda.indexOf( preferencias );
      this.preferenciasEnBusqueda.splice( posicionIndex, 1);
      const toast = this.toastCtrl.create({
        message: preferencias + ' fue agregada exitosamente',
        showCloseButton: true,
        closeButtonText: 'Ok'
      });
      toast.present();

    } else {

      //Eliminando del array de lista
      posicionIndex = this.preferenciasEnLista.indexOf( preferencias );
      this.preferenciasEnLista.splice( posicionIndex, 1);

      const toast = this.toastCtrl.create({
        message: preferencias + ' fue eliminada exitosamente',
        showCloseButton: true,
        closeButtonText: 'Ok'
      });
      toast.present();

    }
  }

    inicializarListas( ){

      this.restapiService.buscarPreferencias( "conexion" )
      .then(data => {
        this.preferenciasEnLista = data;
        console.log(this.preferenciasEnLista);
      });
    }


    filtrarPreferencias(ev: any) {
        //this.inicializarListas(1);
        //Este será el valor que uno escribe en el search bar
        let val = ev.target.value;

        // Si está vació no va a filtrar.
        if (val && val.trim() != '') {
          this.preferenciasEnBusqueda = this.preferenciasEnBusqueda.filter((item) => {
            return (item.toLowerCase().indexOf(val.toLowerCase()) > -1);
          })
        }
      }


}

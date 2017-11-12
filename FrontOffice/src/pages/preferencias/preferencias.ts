import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { ToastController } from 'ionic-angular';
import { Storage } from '@ionic/storage';

@IonicPage()
@Component({
  selector: 'page-preferencias',
  templateUrl: 'preferencias.html',
})
export class PreferenciasPage {

  preferenciasEnLista: any; //Aquí se guardarán los items de preferencias.
  preferenciasEnBusqueda: any; //Aquí se irán guardando los que se traigan de la Base de datos.
  idUsuario: any;
  

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController,
    public restapiService: RestapiService,private storage: Storage) {

    this.cargarListas();

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad PreferenciasPage');
  }

  aviso(str, idPreferencias, nombrePreferencias) {

    var posicionIndex;

    if (str == "agregado") {

      posicionIndex = this.preferenciasEnBusqueda.indexOf( idPreferencias );
      this.preferenciasEnBusqueda.splice( posicionIndex, 1);
      this.restapiService.agregarPreferencias( this.idUsuario ,idPreferencias )
      .then(data => {
        
        if(data != 0)
        {

          this.preferenciasEnLista = data;
          console.log( this.preferenciasEnLista );
        }

      });
      const toast = this.toastCtrl.create({
        message: nombrePreferencias + ' fue agregada exitosamente',
        showCloseButton: true,
        closeButtonText: 'Ok'
      });
      toast.present();

    } else {

      //Eliminando del array de lista
      posicionIndex = this.preferenciasEnLista.indexOf( idPreferencias );
      this.preferenciasEnLista.splice( posicionIndex, 1);
      this.restapiService.eliminarPreferencias( this.idUsuario ,idPreferencias )
      .then(data => {
        
        if(data != 0)
        {

          this.preferenciasEnLista = data;
          console.log( this.preferenciasEnLista );
        }

      });
      const toast = this.toastCtrl.create({
        message: 'La categoria ' + nombrePreferencias + ' fue eliminada exitosamente',
        showCloseButton: true,
        closeButtonText: 'Ok'
      });
      toast.present();

    }
  }


    cargarListas(){

      this.storage.get('id').then((val) => {
        this.idUsuario = val;
        this.inicializarListas();
      });

    }

    inicializarListas( ){

      this.restapiService.buscarPreferencias( this.idUsuario )
      .then(data => {

        if(data != 0)
        {

          this.preferenciasEnLista = data;
          console.log(data);

        }

      });

      
    }


    buscarFiltrado(ev: any) {
        //Este será el valor que uno escribe en el search bar
        let val = ev.target.value;
        if(val.lenght == 0){
          this.preferenciasEnBusqueda = null;
        }else{

                this.restapiService.buscarPreferenciasFiltrado( this.idUsuario , val)
                .then(data => {
          
                  if(data != 0)
                  {
          
                    this.preferenciasEnBusqueda = data;
                    console.log(data);
          
                  }
          
                });
      }
      }


}

import { Component } from '@angular/core';
import { IonicPage, NavController,ModalController,NavParams  } from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { Storage } from '@ionic/storage';

/**
 * Generated class for the DetalleEventoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-detalle-evento',
  templateUrl: 'detalle-evento.html',
})
export class DetalleEventoPage {
eve:any;
apiUrl = 'http://localhost:8091/';
  constructor(private storage: Storage,public navCtrl: NavController,public navParams: NavParams, private modalCtrl: ModalController,public translateService: TranslateService) {
    /*t*his.storage.get('id').then((val) => {
      if(val != null || val != undefined){
        this.translateService.use(val);
      }
    });*/
    this.eve= this.navParams.get('eventos'); 

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad DetalleEventoPage');
  }
closeModal()
  {
      this.navCtrl.pop();
  }
}

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
  selector: 'page-detalle-lt',
  templateUrl: 'detalle-lt.html',
})
export class DetalleLtPage {
  lugares:any;
 // apiUrl = 'http://localhost:8082/';
    constructor(private storage: Storage,public navCtrl: NavController,public navParams: NavParams, private modalCtrl: ModalController,public translateService: TranslateService) {
     /* this.storage.get('id').then((val) => {
        if(val != null || val != undefined){
          this.translateService.use(val);
        }
      });*/
      this.lugares= this.navParams.get('lugares'); 
  
    }
  
    ionViewDidLoad() {
      console.log('ionViewDidLoad DetalleEventoPage');
    }
  closeModal()
    {
        this.navCtrl.pop();
    }
  }
  
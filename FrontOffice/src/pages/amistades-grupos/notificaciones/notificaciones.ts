import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

@Component({
  selector: 'page-notificaciones',
  templateUrl: 'notificaciones.html'
})
export class NotificacionesPage {
  
  lista: Array<notificaciones> = [{ img: 'https://i.pinimg.com/474x/60/d1/3a/60d13adaac4377da28f926af3bfadf8a--icon-design-design-ui.jpg', nick_name: 'Darth Vader'},
  { img: 'https://www.shareicon.net/data/2016/11/21/854794_r2d2_512x512.png', nick_name: 'R2D2' },
  { img: 'https://d13yacurqjgara.cloudfront.net/users/448/screenshots/1705077/screen_shot_2014-08-29_at_2.14.14_pm.png', nick_name: 'Mr. Stormtrooper' }];

  constructor(public navCtrl: NavController) {
   
  }
//comentario
  onLink(url: string) {
    window.open(url);
}


}

interface notificaciones {
  img: string; //Este es el avatar
  nick_name: string; //El nickname
}

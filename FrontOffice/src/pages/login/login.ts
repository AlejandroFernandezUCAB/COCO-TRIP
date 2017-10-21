import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import firebase from 'firebase';
import { Facebook } from '@ionic-native/facebook';
import { FacebookLoginResponse } from "@ionic-native/facebook";
import { HomePage } from '../home/home';
import { RegisterPage } from '../register/register';
import { GoogleAuth, User } from '@ionic/cloud-angular';
import { LoadingController } from 'ionic-angular';
/**
 * Generated class for the LoginPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage {
  userData: any;
  vista: boolean;
  constructor(public navCtrl: NavController,public loadingCtrl: LoadingController, public facebook: Facebook, public googleAuth: GoogleAuth, public user: User,public navParams: NavParams) {
    this.vista=false;
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');
  }
  
  login(){
    this.presentLoadingDefault();
  }
  facebookLogin() {
    
    this.facebook.login(['email', 'public_profile']).then((response: FacebookLoginResponse) => {
      this.facebook.api('me?fields=id,name,email,first_name,picture.width(720).height(720).as(picture_large)', []).then(profile => {
        this.userData = {email: profile['email'], first_name: profile['first_name'], picture: profile['picture_large']['data']['url'], username: profile['name']}
        this.navCtrl.setRoot(HomePage);
      });
    });
  }

  googleLogin(){
    this.googleAuth.login();
    this.navCtrl.setRoot(HomePage);
  }

  registrar(){
    this.navCtrl.push(RegisterPage);
  }

  presentLoadingDefault() {
    const loading = this.loadingCtrl.create({
      content: 'Please wait...',
      duration: 5000
    });
    loading.onDidDismiss(() => {
      this.navCtrl.setRoot(HomePage);
    });
    loading.present();
  }
  Otros(){
    if(this.vista == true)
      this.vista=false;
      else
      this.vista=true;    
  }
  getVista(){
    return(this.vista);
  }
  
}

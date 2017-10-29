import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, AlertController } from 'ionic-angular';
import { Facebook } from '@ionic-native/facebook';
import { FacebookLoginResponse } from "@ionic-native/facebook";
import { HomePage } from '../home/home';
import { RegisterPage } from '../register/register';
import { GoogleAuth, User, AuthLoginResult } from '@ionic/cloud-angular';
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
  constructor(public navCtrl: NavController, public loadingCtrl: LoadingController, public toastCtrl: ToastController, public alertCtrl: AlertController, public facebook: Facebook, public googleAuth: GoogleAuth, public user: User, public navParams: NavParams) {
    this.vista = false;

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');
  }

  login() {
    this.presentLoadingDefault();
  }
  facebookLogin() {

    this.facebook.login(['email', 'public_profile']).then((resultPositivoFacebook: FacebookLoginResponse) => {
      this.facebook.api('me?fields=id,name,email,first_name,picture.width(720).height(720).as(picture_large)', []).then(profile => {
        this.userData = { email: profile['email'], first_name: profile['first_name'], picture: profile['picture_large']['data']['url'], username: profile['name'] }
        this.navCtrl.setRoot(HomePage);
      });
    },
      (resultPositivoFacebook: FacebookLoginResponse) => {
        const toast = this.toastCtrl.create({
          message: 'Error, por favor intente de nuevo',
          duration: 3000,
          position: 'top'
        });
        toast.present();

      }
    );
  }

  googleLogin() {
    this.googleAuth.login().then((resultPositivoGoogle: AuthLoginResult) => this.navCtrl.setRoot(HomePage),
      (resultNegativoGoogle: AuthLoginResult) => {

        const toast = this.toastCtrl.create({
          message: 'Error, por favor intente de nuevo',
          duration: 3000,
          position: 'top'
        });
        toast.present();
      });
  }

  registrar() {
    this.navCtrl.push(RegisterPage);
  }

  presentLoadingDefault() {
    const loading = this.loadingCtrl.create({
      content: 'Por favor, espere...',
      duration: 5000
    });
    loading.onDidDismiss(() => {
      this.navCtrl.setRoot(HomePage);
    });
    loading.present();
  }
  Otros() {
    if (this.vista == true)
      this.vista = false;
    else
      this.vista = true;
  }
  getVista() {
    return (this.vista);
  }

  presentPrompt() {
    const alert = this.alertCtrl.create({
      title: 'Recuperar clave',
      message: 'Introduza su correo para recuperar su clave',
      inputs: [
        {
          name: 'correo',
          placeholder: 'ejemplo@ejemplo.com',
          type: 'email'
        }
      ],
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: data => {
            console.log('Cancel clicked');
          }
        },
        {
          text: 'Enviar',
          handler: data => {
            const toast = this.toastCtrl.create({
              duration: 5000,
              position: 'top'
            });
            if (data.correo) {
              toast.setMessage('Se le ha enviado un correo para recuperar la clave');
              toast.present();
            }
            else{
              toast.setMessage('Por favor, escriba un correo valido');
              toast.present();
              return false;

            }
          }
        }
      ]
    });
    alert.present();
  }
}

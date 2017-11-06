import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, AlertController } from 'ionic-angular';
import { Facebook } from '@ionic-native/facebook';
import { FacebookLoginResponse } from "@ionic-native/facebook";
import { HomePage } from '../home/home';
import { RegisterPage } from '../register/register';
import { GoogleAuth, User, AuthLoginResult } from '@ionic/cloud-angular';
import { LoadingController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
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
  usuario: string;
  clave: string;
  vista: boolean;
  constructor(public navCtrl: NavController, public loadingCtrl: LoadingController, public toastCtrl: ToastController,
     public alertCtrl: AlertController, public facebook: Facebook, public googleAuth: GoogleAuth, public user: User,
     public restapiService: RestapiService, public navParams: NavParams) {
    this.vista = false;
    //this.getUser();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');
  }

  login() {
    this.userData={correo: this.usuario, clave: this.clave};
    console.log("JSON ES: "+JSON.stringify(this.userData));
    //if( usuario tiene @)
    this.restapiService.iniciarSesion(this.userData)
    .then(data => {
      console.log("RESULTADO: "+this.userData);
      if(data==0)
      {
      const toast = this.toastCtrl.create({
        message: 'Error, datos incorrectos',
        duration: 3000,
        position: 'top'
      });
      toast.present();
    }
    else
    {
      this.navCtrl.setRoot(HomePage);
    }
      
    });
    /*else
      this.restapiService.iniciarSesion(this.userData)
    .then(data => {
      console.log("RESULTADO: "+this.userData);
      if(data==0)
      {
      const toast = this.toastCtrl.create({
        message: 'Error, datos incorrectos',
        duration: 3000,
        position: 'top'
      });
      toast.present();
    }
    else
    {
      this.navCtrl.setRoot(HomePage);
    }
      
    });*/
  }
  
  facebookLogin() {

    this.facebook.login(['email', 'public_profile']).then((resultPositivoFacebook: FacebookLoginResponse) => {
      this.facebook.api('me?fields=id,email,first_name,last_name,birthday,picture.width(720).height(720).as(picture_large)', []).then(profile => {
        this.userData = { correo: profile['email'], nombre: profile['first_name'], 
        apellido: profile['last_name'], fechaNacimiento :profile['birthday'],
         foto: profile['picture_large']['data']['url']};
         console.log("JSON ES: "+JSON.stringify(this.userData));
         this.restapiService.iniciarSesion(this.userData)
         .then(data => {
           console.log("RESULTADO: "+this.userData);
           if(data==0)
           {
           const toast = this.toastCtrl.create({
             message: 'Error, datos incorrectos',
             duration: 3000,
             position: 'top'
           });
           toast.present();
         }
         else
         {
           this.navCtrl.setRoot(HomePage);
         }
           
         });
      });
    },
      (resultNegativoFacebook: FacebookLoginResponse) => {
        const toast = this.toastCtrl.create({
          message: 'Error, por favor intente de nuevo',
          duration: 3000,
          position: 'top'
        });
        toast.present();

      }
    );
  }

  googleLogin() 
  {
    this.navCtrl.setRoot(HomePage);
  }

  registrar() 
  {
    this.navCtrl.push(RegisterPage);
  }

  presentLoadingDefault()
   {
    const loading = this.loadingCtrl.create({
      content: 'Por favor, espere...',
      duration: 5000
    });
    loading.onDidDismiss(() => {
      this.navCtrl.setRoot(HomePage);
    });
    loading.present();
  }
  Otros()
   {
    if (this.vista == true)
      this.vista = false;
    else
      this.vista = true;
  }
  getVista() 
  {
    return (this.vista);
  }

  presentPrompt() 
  {
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

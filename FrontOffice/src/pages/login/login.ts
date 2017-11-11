import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, AlertController } from 'ionic-angular';
import { Facebook, FacebookLoginResponse } from '@ionic-native/facebook';
import { HomePage } from '../home/home';
import { RegisterPage } from '../register/register';
import { GoogleAuth, User, AuthLoginResult } from '@ionic/cloud-angular';
import { LoadingController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Storage } from '@ionic/storage';
import { MenuController } from 'ionic-angular';
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
  usuario: string;
  clave: string;
  vista: boolean;
  toast: any;
  userData: any;
  alert: any;
  loading: any;
  myForm: FormGroup;
  constructor(public navCtrl: NavController, public loadingCtrl: LoadingController, public toastCtrl: ToastController,
    public alertCtrl: AlertController, public facebook: Facebook, public googleAuth: GoogleAuth, public user: User,
    public restapiService: RestapiService, public menu: MenuController, public formBuilder: FormBuilder,
    private storage: Storage, public navParams: NavParams) {
    this.myForm = this.formBuilder.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
    this.vista = false;
    this.menu.enable(false);
  }


  login() {
    if (this.myForm.get('password').errors || this.myForm.get('userName').errors)
      this.realizarToast('Por favor, rellene los campos');
    else {
      this.realizarLoading();
      this.restapiService.iniciarSesion(this.usuario, this.clave)
        .then(data => {
          if (data == 0 || data == -1) {
            this.loading.dismiss();
            this.realizarToast('Error, datos incorrectos');

          }
          else {
            this.storage.set('id', data);
            this.navCtrl.setRoot(HomePage);
          }

        });
    }
  }

  realizarToast(mensaje) {
    this.toast = this.toastCtrl.create({
      message: mensaje,
      duration: 3000,
      position: 'top'
    });
    this.toast.present();
  }
  realizarLoading() {
    this.loading = this.loadingCtrl.create({
      content: 'Por favor espere...',
      dismissOnPageChange: true
    });

    this.loading.present();

  }
  facebookLogin() {
    this.realizarLoading();
    this.facebook.getLoginStatus().then(loginstatus => {
      if (loginstatus.status == "connected") {
        this.facebook.logout();
      }
      this.facebook.login(['email', 'public_profile']).then((resultPositivoFacebook: FacebookLoginResponse) => {
        this.facebook.api('me?fields=id,email,first_name,last_name,birthday,picture.width(720).height(720).as(picture_large)', []).then(profile => {
          this.userData = {
            correo: profile['email'], nombre: profile['first_name'],
            apellido: profile['last_name'], fechaNacimiento: new Date(profile['birthday'])
          };
          this.restapiService.iniciarSesionFacebook(this.userData)
            .then(data => {
              this.loading.dismiss();
              if (data == 0 || data == -1) {
                this.realizarToast('Error con los datos de Facebook');
              }
              else {
                this.storage.set('id', data);
                this.navCtrl.setRoot(HomePage);
              }

            });
        });
      },
        (resultNegativoFacebook: FacebookLoginResponse) => {
          this.realizarToast('Error, por favor intente de nuevo');
          this.loading.dismiss();

        }
      ).catch(e => {
        this.realizarToast('Hubo un problema con Facebook, por favor intente mas tarde.');
        this.loading.dismiss();
      });
    });
  }

  googleLogin() {
    this.navCtrl.setRoot(HomePage);
  }

  registrar() {
    this.navCtrl.push(RegisterPage);
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
          role: 'cancel'
        },
        {
          text: 'Enviar',
          handler: data => {
            if (data.correo.includes("@")) {
              this.realizarLoading();
              this.restapiService.recuperarContrasena(data.correo)
                .then(data => {
                  if (data == 0 || data == -1) {
                    this.realizarToast('Error, correo no registrado');

                  }
                  else {
                    this.realizarToast('Se le ha enviado un correo para recuperar la clave');
                  }
                  this.loading.dismiss();
                });
            }
            else {
              this.realizarToast('Por favor, escriba un correo valido');
              return false;
            }
          }
        }
      ]
    });
    alert.present();
  }
}

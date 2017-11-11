import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, AlertController } from 'ionic-angular';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HomePage } from '../home/home';
import { LoginPage } from '../login/login';
import { LoadingController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { MenuController } from 'ionic-angular';
import { Facebook } from '@ionic-native/facebook';
/**
 * Generated class for the RegisterPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-register',
  templateUrl: 'register.html',
})
export class RegisterPage {

  myForm: FormGroup;
  vista: boolean;
  toast: any;
  userData: any;
  alert: any;
  loading: any;


  nombreUsuario: string;
  correo: string;
  nombre: string;
  apellido: string;
  genero: string;
  fechaNacimiento: string;
  foto: ByteString;
  clave: string;
  clavev: string;
  //constructor(public navCtrl: NavController, public navParams: NavParams)
  constructor(public navCtrl: NavController, public loadingCtrl: LoadingController, public toastCtrl: ToastController,
    public alertCtrl: AlertController, public restapiService: RestapiService, public menu: MenuController,
    public formBuilder: FormBuilder, private storage: Storage, public facebook: Facebook, public navParams: NavParams) {
    this.myForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú0-9])+$/), Validators.minLength(5), Validators.maxLength(20)]],
      name: ['', [Validators.required, Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/), Validators.minLength(5), Validators.maxLength(30)]],
      lastName: ['', [Validators.required, Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/), Validators.minLength(5), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(30)]],
      dateBirth: ['', Validators.required],
      password: ['', [Validators.required, Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/), Validators.minLength(5), Validators.maxLength(20)]],
      passwordConfirmation: ['', [Validators.required, Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/), Validators.minLength(5), Validators.maxLength(20)]],
      gender: ['', Validators.required],
    });
    this.facebook.getLoginStatus().then(loginstatus => {
      if (loginstatus.status == "connected") {
        this.facebook.api('me?fields=id,email,first_name,last_name,birthday,picture.width(720).height(720).as(picture_large)', []).then(profile => {
          this.userData = {
            correo: profile['email'], nombre: profile['first_name'],
            apellido: profile['last_name'], fechaNacimiento: new Date(profile['birthday']).toISOString()
          };
          this.correo = this.userData.correo;
          this.nombre = this.userData.nombre;
          this.apellido = this.userData.apellido;
          this.fechaNacimiento = this.userData.fechaNacimiento;
        });
      }
    });

  }




  saveData() {
    //console.log(this.myForm.value);
    alert(JSON.stringify(this.myForm.value));
  }

  registrar() {
    this.realizarLoading();
    if (this.myForm.get('password').errors || this.myForm.get('userName').errors || this.myForm.get('name').errors || this.myForm.get('lastName').errors || this.myForm.get('email').errors || this.myForm.get('dateBirth').errors || this.myForm.get('gender').errors)
      this.realizarToast('Por favor, rellene los campos');
    else {
      if (this.clave == this.clavev) {
        this.foto = "0101";
        this.restapiService.registrarse(this.nombreUsuario, this.correo, this.nombre, this.apellido, this.genero, new Date(this.fechaNacimiento), this.clave, this.foto)
          .then(data => {
            if (data == 0 || data == -1 || data == -2) {
              this.realizarToast('Error, datos incorrectos.');
            }
            else {
              this.realizarToast('Se le ha enviado un correo para confirmar su registro');
              this.navCtrl.setRoot(LoginPage);
            }
            this.loading.dismiss();
          });
      }
      else {
        this.realizarToast('Las Contraseñas no coinciden.');
        this.loading.dismiss();
      }
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



}

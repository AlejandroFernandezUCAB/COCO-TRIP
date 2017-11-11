import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams , ToastController, AlertController} from 'ionic-angular';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { HomePage } from '../home/home';
import { LoginPage} from '../login/login';
import { LoadingController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { MenuController } from 'ionic-angular';

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

  
  nombreUsuario:string;
  correo:string;
  nombre:string;
  apellido:string;
  genero:string;
  fechaNacimiento:Date;
  foto:ByteString;
  clave:string;
  clavev:string;
  //constructor(public navCtrl: NavController, public navParams: NavParams)
  constructor(public navCtrl: NavController, public loadingCtrl: LoadingController, public toastCtrl: ToastController,
    public alertCtrl: AlertController, public restapiService: RestapiService, public menu: MenuController, public formBuilder: FormBuilder,private storage: Storage, public navParams: NavParams) {
    this.myForm = this.formBuilder.group({
      userName: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú0-9])+$/),Validators.minLength(5), Validators.maxLength(20)]],
      name: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/),Validators.minLength(5), Validators.maxLength(30)]],
      lastName: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/),Validators.minLength(5), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.email,Validators.maxLength(30)]],
      dateBirth: ['', Validators.required],
      password: ['', [Validators.required, Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/),Validators.minLength(5), Validators.maxLength(20)]],
      passwordConfirmation: ['', [Validators.required ,Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/),Validators.minLength(5), Validators.maxLength(20)]],
      gender: ['', Validators.required],
    });
  }

  

  saveData(){
    //console.log(this.myForm.value);
    alert(JSON.stringify(this.myForm.value));
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad RegisterPage');
  }

  registrar() {

    if (this.myForm.get('password').errors || this.myForm.get('userName').errors || this.myForm.get('name').errors || this.myForm.get('lastName').errors || this.myForm.get('email').errors || this.myForm.get('dateBirth').errors || this.myForm.get('gender').errors)
          this.realizarToast('Por favor, rellene los campos');
    else {
      if(this.clave==this.clavev)
      {
        this.foto="0101";
        this.realizarLoading();
        this.restapiService.registrarse(this.nombreUsuario,this.correo,this.nombre,this.apellido,this.genero,this.fechaNacimiento,this.clave,this.foto)
          .then(data => {
             if (data == 0 || data == -1 || data == -2) 
             {
               this.loading.dismiss();
               this.realizarToast('Error, datos incorrectos.');
             }
             else 
             {
              this.storage.set('id', data);
              this.navCtrl.setRoot(LoginPage);
             } 
          });
      }
      else{
        this.realizarToast('Las Contraseñas no coinciden.');
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
  compararclave()
  {
    
  }
  
  

}

import { Component, ViewChild } from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { TranslateService } from '@ngx-translate/core';

import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { LoginPage } from '../pages/login/login';
import { PerfilPage } from '../pages/perfil/perfil';
import { AmistadesGruposPage } from '../pages/amistades-grupos/amistades-grupos';
import { EventosActividadesPage } from '../pages/eventos-actividades/eventos-actividades';
import { ItinerarioPage } from '../pages/itinerario/itinerario';
import { ChatPage } from '../pages/chat/chat';
import { RegisterPage } from '../pages/register/register';
import { EditProfilePage } from '../pages/edit-profile/edit-profile';
import { PreferenciasPage } from '../pages/preferencias/preferencias';
import { ConfigPage } from '../pages/config/config';
import { BorrarCuentaPage } from '../pages/borrar-cuenta/borrar-cuenta';
import { ChangepassPage } from '../pages/changepass/changepass';
import {CalendarioPage } from '../pages/calendario/calendario';

@Component({
  templateUrl: 'app.html'
})
export class CocoTrip {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = LoginPage;

  pages: Array<{title: string, component: any}>;

  constructor(public platform: Platform, public statusBar: StatusBar, public splashScreen: SplashScreen, private translateService: TranslateService) {
    this.initializeApp();

    // used for an example of ngFor and navigation
    this.pages = [
      { title: 'Inicio', component: HomePage },
      //{ title: 'Login', component: LoginPage },
      { title: 'Perfil', component: PerfilPage },
      { title: 'Eventos y Actividades', component: EventosActividadesPage },
      { title: 'Itinerario', component: ItinerarioPage },
      { title: 'Amistades y Grupos', component: AmistadesGruposPage },
      { title: 'Chat', component: ChatPage },
      {title: 'Salir',component: LoginPage}
    ];

  }

  initializeApp() {
    this.platform.ready().then(() => {

    //Lenguaje Predeterminado
    this.translateService.setDefaultLang('es');
    this.translateService.use('es');

      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.backgroundColorByHexString("#002d46");
      this.splashScreen.hide();
    });
  }

  openPage(page) {
    // Reset the content nav to have just this page
    // we wouldn't want the back button to show in this scenario
    this.nav.setRoot(page.component);
  }
}

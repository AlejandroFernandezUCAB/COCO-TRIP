import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { AmistadesGruposPage } from './amistades-grupos';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    AmistadesGruposPage,
  ],
  imports: [
    IonicPageModule.forChild(AmistadesGruposPage),
    TranslateModule
  ],
})
export class AmistadesGruposPageModule {}

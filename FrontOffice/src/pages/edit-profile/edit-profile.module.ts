import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TranslateModule } from '@ngx-translate/core'
import { EditProfilePage } from './edit-profile';

@NgModule({
  declarations: [
    EditProfilePage,
  ],
  imports: [
    IonicPageModule.forChild(EditProfilePage),
    TranslateModule
  ],
})
export class EditProfilePageModule {}

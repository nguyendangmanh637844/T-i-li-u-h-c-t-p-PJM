import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SendmailsComponent } from './page/sendmails/sendmails.component';

const routes: Routes = [{
  path: 'email',
  component: SendmailsComponent,
},
{
  path: '**',
  redirectTo: 'email'
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

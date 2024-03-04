import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { BookDetailsComponent } from './features/book-details/book-details.component';
import { NotFoundComponent } from './features/not-found/not-found.component';
import { ServerErrorComponent } from './features/server-error/server-error.component';
import { LibraryComponent } from './features/library/library.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'book/:id', component: BookDetailsComponent},
  {path: 'library', component: LibraryComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},

  //{
    //path: 'manager',
    //loadChildren: () =>
      //import('./features/features.module').then((m) => m.FeaturesModule),
  //},

  {path: '**', redirectTo:'', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpService } from './services/http.service'
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CreateComponent } from './create/create.component'
import { UpdateComponent } from './update/update.component';
import { CreateCarComponent } from './create-car/create-car.component';
import { ChangeCarOwnerComponent } from './car-changeowner/car-changeowner.component';
import { FineCreateComponent } from './create-fine/create-fine.component';
import { CarGetComponent } from './cars-show/cars-show.component';
import { SearchComponent } from './search-result/search-result.component';
import { CreateFineByCar } from './create-fine-by-car/create-fine-by-car.component'
import { CarGetInfoComponent } from './car-fines-info/car-fines-info.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CreateCarComponent,
    CreateComponent,
    UpdateComponent,
    FetchDataComponent,
    ChangeCarOwnerComponent,
    FineCreateComponent,
    CarGetComponent,
    SearchComponent,
    CreateFineByCar,
    CarGetInfoComponent
   
    

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: FetchDataComponent, pathMatch: 'full' },

      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'create', component: CreateComponent },
      { path: 'update/:id', component: UpdateComponent },
      { path: 'create-car/:id', component: CreateCarComponent },
      { path: 'car-changeowner/:id', component: ChangeCarOwnerComponent },
      { path: 'create-fine/:id', component: FineCreateComponent },
      { path: 'cars', component: CarGetComponent },
      { path: 'search-result/:id', component: SearchComponent },
      { path: 'create-fine-by-car/:id', component: CreateFineByCar },
      { path: 'car-fines-info/:id', component: CarGetInfoComponent }
     
     
    
  
    ])
  ],
  providers: [HttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../services/http.service';
import { FinesInputViewModel } from '../create-fine/create-fine.component';

@Component({
  selector: 'app-finebycarcreate',
  templateUrl: './create-fine-by-car.component.html'
})
export class CreateFineByCar implements OnInit {
  public personData: Person = new Person(0, "", "", "");
  public carData: CarDataInput = new CarDataInput("", "", new Date());
  public fineData: FineDataInput = new FineDataInput(0, "", new Date(), "");
  public id: number;
  public fine: FinesInputViewModel = new FinesInputViewModel(false, 0, new Date(), "", "");
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
  //  }
  constructor(private http: HttpService, private route: ActivatedRoute) {

    }
    ngOnInit(): void {
      this.id = +this.route.snapshot.paramMap.get('id');
      
      //  this.http.getPersonsDataFromServer().subscribe((data: PersonData[]) => this.personsData = data);
  }
  async Create() {
    // await this.http.postPerson(this.personData).subscribe((data: Person) => this.personData = data);
    //await this.http.addCarToPerson(this.carData, this.id).subscribe((data => this.http.getPersonsDataFromServer()));
    await this.http.addFineToPerson(this.fineData, this.id).subscribe((data) => this.http.getPersonsDataFromServer());
    location.pathname = '/';

  }
}

interface PersonData {
  
    id: number;
    surname: string;
    city: string;
    address: string;
  //  PersonCars: any;
}
export class Person {
  constructor(public id: number, public surname: string, public city: string, public address: string) {

  }

}
export class CarDataInput {
  constructor(public name: string, public number: string, public date: Date) {

  }
}
export class FineDataInput {
  constructor(public value: number, public number: string, public date: Date, public numberRegistrator:string) {

  }
  
}

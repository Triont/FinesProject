import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../services/http.service';
import { ActivatedRoute } from '@angular/router';
import { createConstructor } from 'typescript';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html'
})
export class UpdateComponent implements OnInit {
  public person: ModelUpdate = new ModelUpdate(0, "", "", "", [], []);
  public id: number;
  public interval: any;

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
  //  }
  constructor(private http: HttpService, private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.id = +this.route.snapshot.paramMap.get('id');
    this.GetData(this.id);
    this.interval = setInterval(() => {
      this.GetData(this.id);
    }, 1000);

    //  this.http.getPersonsDataFromServer().subscribe((data: PersonData[]) => this.personsData = data);
  }
   GetData(id) {
     this.http.getPersonById(id).subscribe((data: ModelUpdate) => this.person = data);
   

  }
  SaveData() {
    this.http.updatePerson(this.person, this.id).subscribe((data => this.http.getPersonsDataFromServer()));
    location.pathname = '/';
  }
  AddCar() {
    location.pathname = '/create-car/' + this.id;
  }
  ChangeOwner(id: number) {
    location.pathname = '/car-changeowner/' + id;
  }
  AddFine() {
    location.pathname = '/create-fine/' + this.id;
  }

  async CloseFine(id: number) {
   await this.http.closeFine(id).subscribe((data) => this.http.getPersonsDataFromServer());
    this.http.getPersonById(this.id).subscribe((data: ModelUpdate) => this.person = data);
  }
}

interface PersonData {

  id: number;
  surname: string;
  city: string;
  address: string;
  //  PersonCars: any;
}
export class ModelUpdate {
  constructor( public id:number, public surname:string, public city:string, public address:string,public carData:Car[], public fineDatas:Fine[]) {

  }
}

export class Car {
  id: number;
  name: string;
  number: string;
  beginOwning: Date;
  endOwning: Date;
}
export class Fine {
  id: number;
  value: number;
  carId: number;
  isActive: boolean;
  number: string;
  

}

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../services/http.service';
import { Router} from '@angular/router'

@Component({
  selector: 'app-carsshow',
  templateUrl: './cars-show.component.html'
})
export class CarGetComponent implements OnInit {
  public personsData: PersonDataOutput[];
  public carsData: CarShowViewModel[];
  public lst: number[]  = new Array();
  public Checked: boolean[];
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
    //  }
    constructor(private http: HttpService) {

    }
  ngOnInit() {
    
    this.http.getCarsDataFromServer().subscribe((data: CarShowViewModel[]) => this.carsData = data);
    setTimeout(() => {                           //<<<---using ()=> syntax
    
    }, 5000);
    
    console.log(this.carsData);
    
      
     // this.Checked.length = this.personsData.length;

  }
  public New() {

  }

  update() {
    
    location.pathname = '/update';
    
  }
  public deleteF() {
   // alert("Alert");

    this.http.deletePerson(this.lst);
    for (var i = 0; i < this.lst.length; i++) {
      this.http.detetePersonById(this.lst[i]).subscribe(data => this.ngOnInit());
    }
 //   this.http.deletePerson(this.lst).subscribe(data => this.ngOnInit());

    console.log(this.personsData);
  }
  Change(v) {
    
    if (!this.lst.includes(v) || this.lst == undefined) {

      this.lst.push(v);
    }
    else {
      var i = this.lst.indexOf(v);
      this.lst.splice(i,1);
    }
   
    
   
    console.log(this.lst);


  };
  CreateNew() {
    location.pathname = '/create';
  }
}

interface PersonData {
  
    id: number;
    name: string;
    city: string;
  address: string;
  
 // person: Person;
  //finedata: string;
 // carsdata: string;
  //  PersonCars: any;
}
interface PersonDataOutput {
  id: number;
  surname: string;
  city: string;
  address: string;
  fineData: string;
  carData: string;

}
interface CarShowViewModel {
  id: number;
  name: string;
  number: string;
  ownerId: number;
  fines: string;
}

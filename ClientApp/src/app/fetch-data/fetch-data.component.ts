import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../services/http.service';
import { Router} from '@angular/router'

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public personsData: Person[];
  public lst: number[]  = new Array();
  public Checked: boolean[];
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
    //  }
    constructor(private http: HttpService) {

    }
    ngOnInit(): void {

      this.http.getPersonsDataFromServer().subscribe((data: Person[]) => this.personsData = data);
      console.log(this.personsData);

      
     // this.Checked.length = this.personsData.length;

  }
  public New() {

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
interface Person {
  id: number;
  name: string;
  city: string;
  address: string;
}

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../services/http.service'

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html'
})
export class CreateComponent implements OnInit {
  public personData: Person = new Person(0, "", "", "");

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
    //  }
    constructor(private http: HttpService) {

    }
    ngOnInit(): void {

      //  this.http.getPersonsDataFromServer().subscribe((data: PersonData[]) => this.personsData = data);
  }
  Create() {
    this.http.postPerson(this.personData).subscribe((data: Person) => this.personData = data);
  }
}

interface PersonData {
  
    id: number;
    name: string;
    city: string;
    address: string;
  //  PersonCars: any;
}
export class Person {
  constructor(public id: number, public name: string, public city: string, public address: string) {

  }
}

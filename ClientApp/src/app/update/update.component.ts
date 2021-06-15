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
  public person: Person = new Person(0, "", "", "", 0, 0);
  public id: number;

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

    //  this.http.getPersonsDataFromServer().subscribe((data: PersonData[]) => this.personsData = data);
  }
   GetData(id) {
     this.http.getPersonById(id).subscribe((data: Person) => this.person = data);
   

  }
  SaveData() {
    this.http.updatePerson(this.person).subscribe((data => this.http.getPersonsDataFromServer()));
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
  constructor(public id: number, public surname: string, public city: string, public address: string,
    public carsCount: number, public finesCount: number) {

  }
}
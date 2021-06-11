import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../services/http.service'

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
    public personsData: PersonData[];

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
    //  }
    constructor(private http: HttpService) {

    }
    ngOnInit(): void {

        this.http.getPersonsDataFromServer().subscribe((data: PersonData[]) => this.personsData = data);
    }
}

interface PersonData {
  
    id: number;
    name: string;
    city: string;
    address: string;
  //  PersonCars: any;
}

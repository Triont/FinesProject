import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class HttpService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //  http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));
    //  }
  }
  getData() {
    return this.http.get('assets/user.json')
  } 
  getMultipleData() {
    return this.http.get('assets/users.json');
  }
  getPersonsDataFromServer() {
    return this.http.get(this.baseUrl+'api/persons');
  }

  deletePerson(arr: number[]) {
    for (var i = 0; i < arr.length; i++) {
      this.detetePersonById(arr[i]);
    }
   
  }

  detetePersonById(id: number) {
    var url = this.baseUrl + 'api/Persons/';
    var n_url = this.baseUrl + 'api/persons/'+id;
    this.http.delete(n_url);
  }
  getCarsDataFromServer() {
    return this.http.get(this.baseUrl + 'api/cars');
  }
  getFinesDataFromServer() {
    return this.http.get(this.baseUrl + 'api/fines');
  }
  postPerson(person: any) {
    var json = JSON.stringify(person);
    return this.http.post(this.baseUrl + 'api/persons/', json);
  }
}

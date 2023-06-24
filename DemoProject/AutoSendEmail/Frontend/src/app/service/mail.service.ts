import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class MailService {

  constructor(public http:HttpClient) { }
  readonly urlAPI ="http://localhost:44312/api";

  send()
  {
    return this.http.get(`${this.urlAPI}/Email/send-email`);
  }
}

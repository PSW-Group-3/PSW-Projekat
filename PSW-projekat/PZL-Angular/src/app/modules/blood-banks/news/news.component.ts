import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BloodBankService } from '../services/blood-bank.service';
import { News } from '../model/news.model';


@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  public dataSource = new MatTableDataSource<News>();
  public displayedColumns = ['name', 'text', 'acceptButton', 'declineButton'];
  public news: News[] = [];
  public errorMessage: any;
  public selectedItem = new News();
  public isSelected: boolean; 

  constructor(private bloodBankService: BloodBankService,private router: Router) { }

  ngOnInit(): void {
    this.isSelected = false;
  let n1 = new News();
  n1.bloodBankId = 123;
  n1.name = 'Dodji i daj krv!';
  this.news.push(n1);
  this.dataSource.data = this.news;
  }

 public  acceptNews(id:number){
  //TODO:
 }

 public declineNews(id:number){
  //TODO:
 }

}

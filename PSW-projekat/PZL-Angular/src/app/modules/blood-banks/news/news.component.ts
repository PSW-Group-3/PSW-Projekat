import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BloodBankService } from '../services/blood-bank.service';
import { News } from '../model/news.model';
import { NewsService } from '../services/news.service';


@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  public dataSource = new MatTableDataSource<News>();
  public displayedColumns = ['name', 'text', 'acceptButton', 'declineButton'];
  public newses: News[] = [];
  public errorMessage: any;
  public selectedItem = new News();
  public isSelected: boolean; 

  constructor(private newsService: NewsService,private router: Router) { }

  ngOnInit(): void {
    this.newsService.getAllPending().subscribe(res =>{
      this.newses = res;
    })
  }

 public  acceptNews(id:number){
  //TODO:
 }

 public declineNews(id:number){
  //TODO:
 }

}

import { elementEventFullName } from '@angular/compiler/src/view_compiler/view_compiler';
import { Component, ElementRef, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ApiBackEndService } from './api/api-back-end-service';
import { Story } from './models/story';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Hacker Feed - Newest Stories';
  pageProcessing = false;
  stories: any;
  filterValue = '';
  dataSource = new MatTableDataSource([]);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns = ['title'];

  constructor(public apiBackEndService: ApiBackEndService) {}

  ngOnInit(): void {
    this.pageProcessing = true;
    this.apiBackEndService.getNewestStories().subscribe((response: [Story]) => {
      this.pageProcessing = false;
      if (response) {
        this.stories = response;
        const newestStories: any = [];

        for (const story of response) {
          if (story.url) {
            newestStories.push(story);
          }
        }
        
        this.dataSource = new MatTableDataSource(newestStories);

        setTimeout (() => {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;

          this.dataSource.sortingDataAccessor = (item, property) => {
            return item[property];
          };
        }, 500);
      } else {
        this.title = 'No stories available right now, try again later!'
      }
    },
    error => {
      this.pageProcessing = false;
      this.title = 'Error retrieving stories: ' + this.formatError(error);
    });
  }

  // apply filter to data table
  applyFilter(filterValue: string): void {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  formatError(error: any): string {
    if (error) {
      if (error.data && error.data.msg != null) {
        return  error.data.msg;
      } else if (error.name != null && error.message != null) {
        return error.name + ' ' + error.message;
      } else if (error.message != null) {
        return error.message;
      } else if (error.status !== undefined && error.status != null) {
        return 'Communication error ( ' + error.status + ')';
      } else if (error.error) {
        if (error.error.msg) {
          return error.error.msg;
        } else {
          return error.error;
        }
      } else if (error instanceof String) {
        return error.toString();
      } else {
        return error;
      }
    }

    return 'unknown';
  }
}

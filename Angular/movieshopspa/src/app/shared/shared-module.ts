import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieCard } from './components/movie-card/movie-card';



@NgModule({
  declarations: [
    MovieCard
  ],
  imports: [
    CommonModule
  ],
  exports: [MovieCard] // Export the MovieCard component to make it available in other modules
})
export class SharedModule { }

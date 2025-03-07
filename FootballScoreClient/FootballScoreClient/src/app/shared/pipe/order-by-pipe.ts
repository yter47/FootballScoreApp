import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'orderBy'
})
export class OrderByPipe implements PipeTransform {
  transform(value: any[], field: string): any[] {
    if (!value || !field) return value;

    const positions = ['Goalkeeper', 'Defence', 'Centre-Back', 'Left-Back', 'Right-Back', 'Midfield', 'Attacking Midfield', 'Centre-Forward', 'Offence', 'Left Winger', 'Right Winger'];

    return value.sort((a, b) => {
      return positions.indexOf(a[field]) - positions.indexOf(b[field]);
    });
  }
}

var x1 >=0 ;
var x2 >=0 ;
var x3 >=0 ;
var x4 >=0 ;
var x5 >=0 ;
var x6 >=0 ;
var x7 >=0 ;
var x8 >=0 ;
var x9 >=0 ;
var x10 >=0 ;
var x11 >=0 ;
var x12 >=0 ;
var x13 >=0 ;
maximize obj: 0.0  + 5.0 * x1   -3.0 * x2   -5.0 * x3   + 5.0 * x4   + 5.0 * x5   -3.0 * x6 ;
c1: x7 = 23.0  -7.0 * x1  -4.0 * x2  -1.0 * x3  -8.0 * x4  -10.0 * x5  -7.0 * x6 ;
c2: x8 = 5.0  + 3.0 * x1  + 8.0 * x2  + 5.0 * x3  + 8.0 * x4  -3.0 * x5  -8.0 * x6 ;
c3: x9 = 7.0  + 4.0 * x1  + 2.0 * x2  + 8.0 * x3  -5.0 * x4  -6.0 * x5  + 1.0 * x6 ;
c4: x10 = 7.0  + 9.0 * x1  -2.0 * x2  -5.0 * x3  + 3.0 * x4  + 1.0 * x5  -7.0 * x6 ;
c5: x11 = -11.0  + 9.0 * x1  + 0.0 * x2  -7.0 * x3  -7.0 * x4  + 9.0 * x5  -9.0 * x6 ;
c6: x12 = -16.0  -1.0 * x1  + 8.0 * x2  + 9.0 * x3  -4.0 * x4  + 4.0 * x5  + 7.0 * x6 ;
c7: x13 = -1.0  -2.0 * x1  + 5.0 * x2  -6.0 * x3  + 1.0 * x4  + 6.0 * x5  -9.0 * x6 ;
solve; 
display 0.0  + 5.0 * x1   -3.0 * x2   -5.0 * x3   + 5.0 * x4   + 5.0 * x5   -3.0 * x6 ;
 
 end; 
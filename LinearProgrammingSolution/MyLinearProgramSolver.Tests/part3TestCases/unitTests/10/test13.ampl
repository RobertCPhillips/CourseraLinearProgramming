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
var x14 >=0 ;
maximize obj: 0.0  + 3.0 * x1   + 1.0 * x2   + 5.0 * x3   -1.0 * x4   -5.0 * x5 ;
c1: x6 = 11.0  -2.0 * x1  + 5.0 * x2  + 2.0 * x3  + 5.0 * x4  -10.0 * x5 ;
c2: x7 = -4.0  + 3.0 * x1  -2.0 * x2  -2.0 * x3  + 0.0 * x4  + 9.0 * x5 ;
c3: x8 = 27.0  + 3.0 * x1  -6.0 * x2  -2.0 * x3  -4.0 * x4  -8.0 * x5 ;
c4: x9 = 7.0  -10.0 * x1  + 10.0 * x2  -3.0 * x3  + 10.0 * x4  -6.0 * x5 ;
c5: x10 = 20.0  + 7.0 * x1  -9.0 * x2  -2.0 * x3  + 1.0 * x4  -3.0 * x5 ;
c6: x11 = 15.0  + 8.0 * x1  + 0.0 * x2  -2.0 * x3  -1.0 * x4  -9.0 * x5 ;
c7: x12 = 18.0  + 1.0 * x1  -7.0 * x2  -10.0 * x3  -4.0 * x4  + 4.0 * x5 ;
c8: x13 = -4.0  + 10.0 * x1  + 3.0 * x2  + 3.0 * x3  + 7.0 * x4  + 0.0 * x5 ;
c9: x14 = -5.0  + 4.0 * x1  -7.0 * x2  -5.0 * x3  + 8.0 * x4  + 10.0 * x5 ;
solve; 
display 0.0  + 3.0 * x1   + 1.0 * x2   + 5.0 * x3   -1.0 * x4   -5.0 * x5 ;
 
 end; 

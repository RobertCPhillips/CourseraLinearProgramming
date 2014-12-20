library(Rglpk)

#----------------------------------------------------------
#q1
#----------------------------------------------------------
# 3 SAT constraints -> ILP

#  x1 V  x2 V -x3 -> y1 + y2 + (1 - y3) >= 1
#  x1 V  x2 V  x3 -> y1 + y2 + y3 >= 1
#  x1 V -x2 V -x3 -> y1 + (1 - y2) + (1 - y3) >= 1
#  x1 V -x2 V  x3 -> y1 + (1 - y2) + y3 >= 1
# -x1             -> (1-y1) >= 1

# min y1 + y2 + y3

# glpk formulation:
obj <- c(1,1,1)
mat <- matrix(c( 1,  1,  1,  1, -1,
                 1,  1, -1, -1,  0,
                -1,  1, -1,  1,  0), nrow=5)
dir <- c(">=", ">=", ">=", ">=", ">=")
rhs <- c(0, 1, -1, 0, 0)
types <- c("B", "B", "B")
max <- F

sol <- Rglpk_solve_LP(obj, mat, dir, rhs, types=types, max=max,
                      control=list("verbose"=T))

#----------------------------------------------------------
#q4 - MAX SAT variation of #q1
#----------------------------------------------------------
# 3 SAT constraints -> ILP

#  x1 V  x2 V -x3 -> y1 + y2 + (1 - y3) >= z1
#  x1 V  x2 V  x3 -> y1 + y2 + y3 >= z2
#  x1 V -x2 V -x3 -> y1 + (1 - y2) + (1 - y3) >= z3
#  x1 V -x2 V  x3 -> y1 + (1 - y2) + y3 >= z4
# -x1             -> (1-y1) >= z5

obj <- c(0,0,0,1,1,1,1,1)
mat <- matrix(c(  1,  1,  1,  1, -1,
                  1,  1, -1, -1,  0,
                 -1,  1, -1,  1,  0,
                 -1,  0,  0,  0,  0,
                  0, -1,  0,  0,  0,
                  0,  0, -1,  0,  0,
                  0,  0,  0, -1,  0,
                  0,  0,  0,  0, -1), nrow=5)

dir <- c(">=", ">=", ">=", ">=", ">=")
rhs <- c(-1, 0, -2, -1, -1)
types <- c("B", "B", "B", "B", "B", "B", "B", "B")

max <- T

sol <- Rglpk_solve_LP(obj, mat, dir, rhs, types=types, max=max,
                      control=list("verbose"=T))


#----------------------------------------------------------
# q9 - We would like to minimize the number of super-markets built.
#      Each town must have a super-market within a 20 minute driving time
#
# resulting graph is:
#
# 1 2 3 4
# 2 1 3 6
# 3 1 2 4
# 4 3 6 7
# 5 1 7
# 6 2 4
# 7 4 5
#
#----------------------------------------------------------
obj <- c(1,1,1,1,1,1,1)

mat <- matrix(c(  1,  1,  1,  0,  1,  0,  0,
                  1,  1,  1,  0,  0,  1,  0,
                  1,  1,  1,  1,  0,  0,  0,
                  0,  0,  1,  1,  0,  1,  1,
                  1,  0,  0,  0,  1,  0,  1,
                  0,  1,  0,  1,  0,  1,  0,
                  0,  0,  0,  1,  1,  0,  1), nrow=7)


dir <- c(">=", ">=", ">=", ">=", ">=", ">=", ">=")
rhs <- c(1,1,1,1,1,1,1)
types <- c("B", "B", "B", "B", "B", "B", "B")

max <- F

sol <- Rglpk_solve_LP(obj, mat, dir, rhs, types=types, max=max,
                      control=list("verbose"=T))


#----------------------------------------------------------
#q10 - variation of q9 - take into account the cost of constructions 
#----------------------------------------------------------
obj <- c(12,8,12,10,9,10,7)

sol <- Rglpk_solve_LP(obj, mat, dir, rhs, types=types, max=max,
                      control=list("verbose"=T))












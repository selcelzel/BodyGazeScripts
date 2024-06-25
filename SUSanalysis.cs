scores <- c(57.5, 87.5, 67.5, 82.5, 75, 75, 92.5, 60, 50, 95, 
            87.5, 70, 82.5, 72.5, 85, 80, 67.5, 75, 87.5, 82.5, 
            65, 47.5, 95, 82.5, 92.5, 65, 45, 82.5, 77.5, 72.5, 
            87.5, 87.5, 67.5, 77.5, 77.5, 70, 75, 82.5, 75, 82.5)

gamification <- scores[1:20]
control <- scores[21:40]

library(car)

levene_test <- leveneTest(scores ~ factor(c(rep("gamification", 20), rep("control", 20))))
print(levene_test)

t_test <- t.test(gamification, control, var.equal = TRUE)

print(t_test)
data <- read.csv("C:/Users/hp/Desktop/dataset.xlsx", header = TRUE)

mean_gamex <- mean(data$final_score)
median_gamex <- median(data$final_score)
mode_gamex <- as.numeric(names(sort(table(data$final_score), decreasing = TRUE))[1])


output_gamex <- paste("Descriptives for GAMEX column:\n",
                      paste("Mean:", round(mean_gamex, 2)),
                      paste("Median:", median_gamex),
                      paste("Mode:", mode_gamex),
                      "\n\n")


mean_enjoyment <- mean(rowSums(data[, 1:6]))
mean_absorption <- mean(rowSums(data[, 7:12]))
mean_creative <- mean(rowSums(data[, 13:16]))
mean_activation <- mean(rowSums(data[, 17:19]))
mean_negative_affect <- mean(rowSums(data[, 20:22]))
mean_dominance <- mean(rowSums(data[, 23:25]))


output_subscales <- paste("Descriptives for Subscales:\n",
                          paste("Mean Enjoyment:", round(mean_enjoyment, 6)),
                          paste("Mean Absorption:", round(mean_absorption, 6)),
                          paste("Mean Creative Thinking:", round(mean_creative, 6)),
                          paste("Mean Activation:", round(mean_activation, 6)),
                          paste("Mean Absence of Negative Affect:", round(mean_negative_affect, 6)),
                          paste("Mean Dominance:", round(mean_dominance, 6)),
                          "\n\n")


cat(output_gamex)
cat(output_subscales)

# Write results to a text file
output_file <- "descriptive_stats.txt"
writeLines(c(output_gamex, output_subscales), output_file)

cat("Results have been saved to", output_file, "\n")
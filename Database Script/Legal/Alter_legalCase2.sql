Alter table LegalCases
drop column NatureOfCase,SummaryOfCase
go
Alter table LegalCases
Add NatureOfCase varchar(max),
SummaryOfCase  varchar(max)
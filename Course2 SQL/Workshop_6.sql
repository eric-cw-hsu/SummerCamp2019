USE GSSWEB

SELECT pvt.ClassId,
		pvt.ClassName,
		ISNULL(pvt.[2016], 0) as CNT2016,
		ISNULL(pvt.[2017], 0) as CNT2017,
		ISNULL(pvt.[2018], 0) as CNT2018,
		ISNULL(pvt.[2019], 0) as CNT2019
			
			
FROM(

	SELECT bd.BOOK_CLASS_ID as ClassId,
			bc.BOOK_CLASS_NAME as ClassName,
			YEAR(LEND_DATE) AS [year],
			count(YEAR(LEND_DATE)) as cnt
		
	FROM BOOK_LEND_RECORD as blr
	INNER JOIN BOOK_DATA as bd
	ON blr.BOOK_ID = bd.BOOK_ID
	INNER JOIN BOOK_CLASS as bc
	ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
	Group by bc.BOOK_CLASS_NAME, bd.BOOK_CLASS_ID, year(LEND_DATE)
) tmp
	PIVOT (SUM(cnt)
		FOR tmp.[year] IN ([2016], [2017], [2018], [2019]) 
		) pvt

ORDER BY pvt.ClassID

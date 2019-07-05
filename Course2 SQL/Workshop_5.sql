USE GSSWEB

SELECT tmp.ClassID, 
		tmp.BookClass,
		SUM(CASE tmp.[year] 
			WHEN 2016 THEN tmp.Cnt ELSE 0 END )AS 'CNT2016',
		SUM(CASE tmp.[year] 
			WHEN 2017 THEN tmp.Cnt ELSE 0 END )AS 'CNT2017',
		SUM(CASE tmp.[year] 
			WHEN 2018 THEN tmp.Cnt ELSE 0 END )AS 'CNT2018',
		SUM(CASE tmp.[year] 
			WHEN 2019 THEN tmp.Cnt ELSE 0 END )AS 'CNT2019' 
FROM(

	SELECT bd.BOOK_CLASS_ID as ClassID,
			bc.BOOK_CLASS_NAME as BookClass,
			YEAR(LEND_DATE) AS [year],
			count(YEAR(LEND_DATE)) as cnt
		
	FROM BOOK_LEND_RECORD as blr
	INNER JOIN BOOK_DATA as bd
	ON blr.BOOK_ID = bd.BOOK_ID
	INNER JOIN BOOK_CLASS as bc
	ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
	GROUP BY bc.BOOK_CLASS_NAME, bd.BOOK_CLASS_ID, YEAR(LEND_DATE)
) tmp


GROUP BY tmp.ClassID, tmp.BookClass
ORDER BY tmp.ClassID

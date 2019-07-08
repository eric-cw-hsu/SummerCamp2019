USE  GSSWEB
SELECT tmp.Seq, tmp.BookClass, tmp.BookID, tmp.BookName, tmp.Cnt
FROM (
	SELECT ROW_NUMBER() OVER (PARTITION BY bc.BOOK_CLASS_NAME ORDER BY  COUNT(blr.BOOK_ID)DESC) AS Seq,
	BOOK_CLASS_NAME AS BookClass,
	bd.BOOK_ID AS BookID,
	bd.BOOK_NAME AS BookName,
	COUNT(blr.BOOK_ID) AS Cnt 

	FROM BOOK_DATA AS bd
	INNER JOIN BOOK_LEND_RECORD AS blr
	ON bd.BOOK_ID = blr.BOOK_ID
	INNER JOIN BOOK_CLASS AS bc
	ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID

	GROUP BY BOOK_NAME, bd.BOOK_ID, BOOK_CLASS_NAME
)tmp

WHERE tmp.Seq <= 3
ORDER BY BookCLass, Cnt DESC, BookID
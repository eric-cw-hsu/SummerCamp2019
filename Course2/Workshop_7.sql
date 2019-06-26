USE GSSWEB

--書本id
--購書日期 BOOK_DATA(BOOK_BOUGHT_DATE)
--借閱日期 BOOK_LEND_RECORD(LEND_DATE)
--書籍類別 BOOK_CLASS(BOOK_CLASS_NAME)
--借閱人 BOOK_MEMBER(USER_CNAME)
--狀態 BOOK_CODE(CODE_NAME)
--購書金額 BOOK_DATA(BOOK_AMOUNT)

SELECT tmp.BOOK_ID as 書本ID,
		FORMAT(tmp.BOUGHT_DATE, 'yyyy-MM-dd') as 購書日期, 
		FORMAT(tmp.LEND_DATE, 'yyyy-MM-dd') as 借閱日期,
		tmp.BOOK_CLASS_NAME as 書籍類別,
		tmp.NAME as 借閱人,  
		tmp.CODE_NAME as 狀態,
		convert(varchar, tmp.BOOK_AMOUNT) + ' 元' as 購書金額

FROM
(
	select BD.BOOK_ID as BOOK_ID,
			BD.BOOK_BOUGHT_DATE as BOUGHT_DATE,
			BLR.LEND_DATE as LEND_DATE,
			BC.BOOK_CLASS_ID + '-' + BC.BOOK_CLASS_NAME as BOOK_CLASS_NAME,
			MM.[USER_ID] + '-' +MM.USER_CNAME + '(' + MM.USER_ENAME + ')' as NAME,
			BD.BOOK_STATUS + '-' + BCD.CODE_NAME as CODE_NAME,
			BD.BOOK_AMOUNT as BOOK_AMOUNT,
			MM.[USER_ID] as [USER_ID]

	FROM BOOK_DATA as BD
	INNER JOIN BOOK_CLASS as BC
	on BD.BOOK_CLASS_ID = BC.BOOK_CLASS_ID

	INNER JOIN BOOK_CODE as BCD
	on BD.BOOK_STATUS = BCD.CODE_ID

	INNER JOIN BOOK_LEND_RECORD as BLR
	on BD.BOOK_ID = BLR.BOOK_ID

	INNER JOIN MEMBER_M as MM
	on MM.[USER_ID] = BLR.KEEPER_ID
) tmp

where tmp.[USER_ID] = '0002'
ORDER BY BOOK_AMOUNT DESC
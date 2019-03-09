CREATE DEFINER=`subasta`@`%` TRIGGER `TRG_VALIDADOR_PUJA` BEFORE INSERT ON `tbl_pujas` FOR EACH ROW BEGIN
	DECLARE pujamaxima DECIMAL(15,5);
	DECLARE loteId INT(11);    
	SELECT CODIGO_LOTE_PUJADOR INTO loteId FROM subasta.tbl_pujadores AS tpu WHERE tpu.ID_PUJADOR = NEW.COD_SUBASTA_PUJADOR;
    SELECT IFNULL(MAX(p.VALOR_PUJA), 0) INTO pujamaxima FROM subasta.tbl_pujas AS p
	JOIN  subasta.tbl_pujadores AS pu
	on p.COD_SUBASTA_PUJADOR = pu.ID_PUJADOR
    join subasta.tbl_lotes as l
    on pu.CODIGO_LOTE_PUJADOR = l.CODIGO_LOTE
    WHERE pu.CODIGO_LOTE_PUJADOR = loteId;
    IF pujamaxima > NEW.VALOR_PUJA OR pujamaxima = NEW.VALOR_PUJA THEN 
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Valor actualizado';
	END IF;
END
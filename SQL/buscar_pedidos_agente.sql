SELECT
c.nomclien AS CLIENTE,
ag.nomagent AS AGENTE,
                    sc.numpedcl AS NUMPEDCL,
                    sc.codclien AS CODCLIEN,
                    sc.fecpedcl AS FECPEDCL,
                    sc.fecentre AS FECENTRE,
                    sl2.total AS TOTALPED,
                    sl.numlinea AS NUMLINEA,
                    sl.codartic AS CODARTIC,
                    sl.nomartic AS NOMARTIC,
                    sl.precioar AS PRECIOAR,
                    sl.cantidad AS CANTIDAD,
                    sl.servidas AS SERVIDAS,
                    sl.dtoline1 AS DTOLINE1,
                    sl.dtoline2 AS DTOLINE2,
                    sl.importel AS IMPORTEL
                    FROM scaped AS sc
                    LEFT JOIN sclien AS c ON c.codclien = sc.codclien
                    LEFT JOIN sagent AS ag ON ag.codagent = c.codagent
                    LEFT JOIN sliped AS sl ON sl.numpedcl = sc.numpedcl
                    LEFT JOIN (SELECT numpedcl, SUM(importel) AS total
                    FROM sliped
                    GROUP BY numpedcl) AS sl2 ON sl2.numpedcl = sc.numpedcl
                    WHERE c.codagent = 6
                    ORDER BY sc.fecpedcl DESC,sc.numpedcl,sl.numlinea;


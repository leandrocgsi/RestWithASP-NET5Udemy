FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithASPNETErudio/db/migrations/ /home/database/
COPY ./RestWithASPNETErudio/db/dataset/ /home/database/
COPY ./RestWithASPNETErudio/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh
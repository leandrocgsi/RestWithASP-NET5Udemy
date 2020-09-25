FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithASPNETUdemy/db/migrations/ /home/database/
COPY ./RestWithASPNETUdemy/db/dataset/ /home/database/
COPY ./RestWithASPNETUdemy/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh
#!/bin/bash

SAMPLEDIR=".Sample.Microservice"
SAMPLENAME="Sample"
SAMPLENAMEMIN="Sample"
SAMPLEAUTHDIR=".SampleAuth.Microservice"
SAMPLEAUTHNAME="SampleAuth"
SAMPLEAUTHNAMEMIN="SampleAuth"
OPDIR="Operations"
SEDIR="Services"
REDIR="Repositories"

#create project
create_project(){
    name=$1
    echo "project creation ..."
    cp -r "$SAMPLEDIR" "$name.Microservice"
    echo "project created ..."
}

#renommer le projet
rename_project(){
    name=$1
    lowerName=${name,,}
    dotnet run -p ./.RenameUtility/RenameUtility -- "$name.Microservice" $SAMPLENAME $name
    dotnet run -p ./.RenameUtility/RenameUtility -- "$name.Microservice" ${SAMPLENAME,,} $lowerName
}

create_project_with_auth(){
    name=$1
    echo "project creation ..."
    cp -r "$SAMPLEAUTHDIR" "$name.Microservice"
    echo "project created ..."
}

#renommer le projet
rename_project_with_auth(){
    name=$1
    lowerName=${name,,}
    dotnet run -p ./.RenameUtility/RenameUtility -- "$name.Microservice" $SAMPLEAUTHNAME $name
    dotnet run -p ./.RenameUtility/RenameUtility -- "$name.Microservice" ${SAMPLEAUTHNAME,,} $lowerName
}

#suppression d'un projet
delete_project(){
    name=$1
    rm -r "${name}.Microservice"
    remove_in_solution $name
    build_project
}


#creation des differents dossiers du service
create_folders(){
    name=$2
    projet=$1
    echo "folders creation ..."
    #creation de l'opération
    cp -r "$SAMPLEDIR/$OPDIR/samples" "$projet.Microservice/$OPDIR/${name}"
    echo "$projet.Microservice/$OPDIR/${name} created"
    
    #create service
    cp -r "$SAMPLEDIR/$SEDIR/samples" "$projet.Microservice/$SEDIR/${name}"
    echo "$projet.Microservice/$SEDIR/${name} created"
    
    #create repository
    cp -r "$SAMPLEDIR/$REDIR/samples" "$projet.Microservice/$REDIR/${name}"
    echo "$projet.Microservice/$REDIR/${name}Repository created"

}

delete_folders(){
    projet=$1
    name=$2

    echo "folders deletion ..."
    #creation de l'opération
    rm -r "$projet.Microservice/$OPDIR/${name}"
    echo "$projet.Microservice/$OPDIR/${name} deleted"

   
    
    #create service
    rm -r "$projet.Microservice/$SEDIR/${name}"
    echo "$projet.Microservice/$SEDIR/${name} deleted"

    
    #create repository
    rm -r "$projet.Microservice/$REDIR/${name}"
    echo "$projet.Microservice/$REDIR/${name} deleted"


    echo "folders deleted"
}

#renommage des differents dossiers créés
rename_folders(){
    projet=$1
    name=$2
    lowerName=${name,,}
    lowerName=${lowerName%?}
    echo "renaming..."
    #rename des opérations
    dotnet run -p ./.RenameUtility/RenameUtility -- "$projet.Microservice/$OPDIR/${name}" "Sample" ${name%?}
    dotnet run -p ./.RenameUtility/RenameUtility -- "$projet.Microservice/$OPDIR/${name}" "sample" $lowerName

    #rename des services
    dotnet run -p ./.RenameUtility/RenameUtility -- "$projet.Microservice/$SEDIR/${name}" "Sample" ${name%?}
    dotnet run -p ./.RenameUtility/RenameUtility -- "$projet.Microservice/$SEDIR/${name}" "sample" $lowerName

    #rename des repositories
    dotnet run -p ./.RenameUtility/RenameUtility -- "$projet.Microservice/$REDIR/${name}" "Sample" ${name%?}
    dotnet run -p ./.RenameUtility/RenameUtility -- "$projet.Microservice/$REDIR/${name}" "sample" $lowerName


    dotnet run -p ./.RenameUtility/RenameUtility -- "$projet.Microservice/" "${name%?}.Microservice" "$projet.Microservice"
	echo "end."
}


main(){
    #creation du dossier de projet
    create_folders $1 $2
    #renommage des différents elements
    rename_folders $1 $2
}

main_project(){
    if [ $# -ge 2 ]
    then
        echo "create with authentification ..."
        #create project
        create_project_with_auth $1 
        #renommage du projet
        rename_project_with_auth $1
        #ajouter un projet à la solution
        add_in_solution $1
        #build projet
        build_project
    else
        echo "create without authentification ..."
        #create project
        create_project $1 
        #renommage du projet
        rename_project $1
        #ajouter un projet à la solution
        add_in_solution $1
        #build projet
        build_project
    fi
}

build_project(){
    dotnet build Microservice.WebApi.sln
}

add_in_solution(){
    dotnet sln "Microservice.WebApi.sln" add "${1}.Microservice/${1}.Microservice.csproj"
}

remove_in_solution(){
    dotnet sln "Microservice.WebApi.sln" remove "${1}.Microservice/${1}.Microservice.csproj"
}



#run du projet
if [ $# -ge 2 ]
then
    case $1 in
	add-service)
        # echo "$2.Microservice/$OPDIR $3"
	    main $2 $3
	    ;;
	delete-service)
	    delete_folders $2 $3
	    ;;
	add-project)
        if [ $# -eq 3 ]
        then
            if [ $3 = "--with-auth" -o $3 = "-wa" ]
            then 
	            main_project $2 $3
            else
                echo "
                    OPTIONS: 
                        --with-auth or -wa : create project with authentification based admin role
                    "
            fi
        else
            main_project $2
        fi
	    ;;
	delete-project)
	    delete_project $2
	    ;;
    rename)
        echo "$2 $3"
	    dotnet run -p ./.RenameUtility/RenameUtility -- "./" $2 $3

        echo "${2,,} ${3,,}"
	    dotnet run -p ./.RenameUtility/RenameUtility -- "./" ${2,,} ${3,,}
     
        build_project
	    ;;
    rename-in)
        echo "$2 $3 $4"
	    dotnet run -p ./.RenameUtility/RenameUtility -- $2 $3 $4

        echo "$2 ${3,,} ${4,,}"
	    dotnet run -p ./.RenameUtility/RenameUtility -- $2 ${3,,} ${4,,}
     
        build_project
	    ;;
    echo)
        echo "${2}.Microservice/${2}.Microservice.csproj"
        ;;
	*)
	    echo "run <COMMAND> : \n 
                add-project [PROJECT_NAME] [--OPTIONS] \n
                add-service [PROJECT_NAME] [SERVICE_NAME] \n
                delete [FOLDER_NAME] \n
                delete-service [PROJECT_NAME] [SERVICE_NAME] \n
                delete-project [PROJECT_NAME] \n
                rename [OLD_NAME] [NEW_NAME] \n
                rename-in [PROJECT_NAME] [OLD_NAME] [NEW_NAME]
                
                
            OPTIONS: 
                --with-auth or -wa : create project with authentification based admin role
                "
	    ;;
   esac
		   
else
    echo "
            run <COMMAND> :  
                add-project [PROJECT_NAME] [--OPTIONS]
                add-service [PROJECT_NAME] [SERVICE_NAME] 
                delete [FOLDER_NAME] 
                delete-service [PROJECT_NAME] [SERVICE_NAME] 
                delete-project [PROJECT_NAME] 
                rename [OLD_NAME] [NEW_NAME] 
                rename-in [PROJECT_NAME] [OLD_NAME] [NEW_NAME]


            OPTIONS: 
                --with-auth or -wa : create project with authentification based admin role
                
        "
fi


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass {

    string CharacterName;
    int characterType,CharacterRole;
    GameObject CharactersHouse;
    

	public CharacterClass()
    {
        CharacterName = "";
        characterType = 0;
        CharacterRole = 0;
        CharactersHouse = null;
    }

    public void SetCharacterName(string name)
    {
        CharacterName = name;
    }

    public void SetCharacterType(int type)
    {
        characterType = type;
    }

    public void SetCharacterRole(int type)
    {
        CharacterRole = type;
    }

    public void SetCharacterName(GameObject hObject)
    {
        CharactersHouse = hObject;
    }

    string GetCharacterName()
    {
        return CharacterName;
    }

    int getCharacterRole()
    {
        return CharacterRole;
    }

    int getCharacterType()
    {
        return characterType;
    }

    GameObject getCharacterHouse()
    {
        return CharactersHouse;
    }






}

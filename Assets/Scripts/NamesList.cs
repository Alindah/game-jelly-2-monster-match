using UnityEngine;

public enum Names
{
    quasi,
    modo,
    lexi,
    poo,
    pee,
    max,
    boo,
    sam,
    tess,
    rod,
    hot,
    lo,
    ali,
    kay,
    kara,
    jar,
    ryn,
    jake,
    jess,
    fin,
    kage,
    kil,
    mark,
    mat,
    ass,
    bad,
    god,
    davi,
    goo,
    jaz,
    min,
    mer,
    maid,
    ran,
    jay,
    co,
    kiko,
    un,
    eil,
    den,
    pa,
    ma,
    end,
    fat,
    hiro,
    ivy,
    nope,
    sad,
    urse,
    vile,
    wal,
    xeno,
    yarv,
    zip,
    maxi,
    pad,
    awan,
    da,
    bomb,
    shell,
    fish,
    styx,
    flex,
    butt,
    face,
    boob,
    titi,
    nis,
    viv,
    con,
    rico,
    suave
}

public class NamesList : MonoBehaviour
{
    public static string GenerateRandomName(int numOfWords = 2)
    {
        string randomName = "";

        for (int i = 0; i < numOfWords; i++)
            randomName += (Names)Random.Range(0, System.Enum.GetValues(typeof(Names)).Length);

        randomName = char.ToUpper(randomName[0]) + randomName[1..];

        return randomName;
    }
}
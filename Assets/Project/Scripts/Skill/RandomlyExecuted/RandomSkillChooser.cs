namespace ReGaSLZR.EndlessRunner.Skill.Random
{
    
    using UnityEngine;

    public class RandomSkillChooser : MonoBehaviour
    {

        [SerializeField]
        private BaseRandomExecSkill[] skills;

        private int randomIndex;

        private void OnEnable()
        {
            DisableAllRandomSkills();

            randomIndex = Random.Range(0, skills.Length - 1);
            skills[randomIndex].SetIsChosen(true);
        }

        private void DisableAllRandomSkills()
        {
            foreach (var skill in skills)
            {
                if (skill != null)
                {
                    skill.SetIsChosen(false);
                }
            }
        }

        public string GetRandomSkillName()
        {
            return skills[randomIndex].GetSkillName();
        }

    }

}